﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Xaml
{
	static class MemberExpressionExtensions
	{
		static ParameterExpression s_InstanceExpression = Expression.Parameter(typeof(object), "instance");
		static ParameterExpression s_ValueExpression = Expression.Parameter(typeof(object), "value");
		static ParameterExpression s_KeyExpression = Expression.Parameter(typeof(object), "key");
		static ParameterExpression[] s_ParameterExpressions1 = { s_InstanceExpression, s_ValueExpression };
		static ParameterExpression[] s_ParameterExpressions2 = { s_InstanceExpression, s_KeyExpression, s_ValueExpression };
		static Type s_TargetExceptionType = typeof(Assembly).GetTypeInfo().Assembly.GetType("System.Reflection.TargetException");

		static Exception TargetException(Type targetType, Type instanceType)
		{
			return s_TargetExceptionType != null
				? (Exception)Activator.CreateInstance(s_TargetExceptionType)
				: new InvalidOperationException($"Instance of type {instanceType} is not assignable to target type {targetType}");
		}

#if PCL259
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		static void CheckParameters(Type declaringType, object instance)
		{
			var instanceType = instance.GetType();
			if (!declaringType.GetTypeInfo().IsAssignableFrom(instanceType.GetTypeInfo()))
				throw TargetException(declaringType, instanceType);
		}

#if PCL259
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		static void CheckParameters(Type declaringType, Type propertyType, object instance, object value)
		{
			var instanceType = instance.GetType();
			if (!declaringType.GetTypeInfo().IsAssignableFrom(instanceType.GetTypeInfo()))
				throw TargetException(declaringType, instanceType);
			if (value != null && !propertyType.GetTypeInfo().IsAssignableFrom(value.GetType().GetTypeInfo()))
				throw new ArgumentException($"Value of type {value.GetType()} cannot be assigned to member with type {propertyType}", nameof(value));
		}

#if PCL259
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		static void CheckParameters(Type declaringType, Type keyType, Type propertyType, object instance, object key, object value)
		{
			var instanceType = instance.GetType();
			if (!declaringType.GetTypeInfo().IsAssignableFrom(instanceType.GetTypeInfo()))
				throw TargetException(declaringType, instanceType);
			if (value != null && !propertyType.GetTypeInfo().IsAssignableFrom(value.GetType().GetTypeInfo()))
				throw new ArgumentException($"Value of type {value.GetType()} cannot be assigned to member with type {propertyType}", nameof(value));
			if (key != null && !keyType.GetTypeInfo().IsAssignableFrom(key.GetType().GetTypeInfo()))
				throw new ArgumentException($"Key of type {key.GetType()} cannot be assigned to member with type {keyType}", nameof(key));
		}

		public static Func<object, object> BuildGetExpression(this MethodInfo getter)
		{
			var declaringType = getter.DeclaringType;

			var instanceCast = !declaringType.GetTypeInfo().IsValueType
				? Expression.TypeAs(s_InstanceExpression, declaringType)
				: Expression.Convert(s_InstanceExpression, declaringType);

			Expression block = Expression.TypeAs(Expression.Call(instanceCast, getter), typeof(object));

			var exp = Expression.Lambda<Func<object, object>>(block, s_InstanceExpression).Compile();

			return (instance) =>
			{
				CheckParameters(declaringType, instance);
				return exp(instance);
			};
		}

		public static Action<object, object> BuildCallExpression(this MethodInfo method)
		{
			var parameters = method.GetParameters();
			Type declaringType;
			Type propertyType;
			if (method.IsStatic)
			{
				if (parameters.Length != 2)
					throw new ArgumentOutOfRangeException(nameof(method), "Static method must have two parameters");
				declaringType = parameters[0].ParameterType;
				propertyType = parameters[1].ParameterType;
			}
			else
			{
				if (parameters.Length != 1)
					throw new ArgumentOutOfRangeException(nameof(method), "Instance method must have one parameter");
				declaringType = method.DeclaringType;
				propertyType = parameters[0].ParameterType;
			}

			// value as T is slightly faster than (T)value, so if it's not a value type, use that
			var instanceCast = !declaringType.GetTypeInfo().IsValueType
				? Expression.TypeAs(s_InstanceExpression, declaringType)
				: Expression.Convert(s_InstanceExpression, declaringType);

			var valueCast = !propertyType.GetTypeInfo().IsValueType
				? Expression.TypeAs(s_ValueExpression, propertyType)
				: Expression.Convert(s_ValueExpression, propertyType);

			Expression block;
			if (method.IsStatic)
				block = Expression.Call(method, instanceCast, valueCast);
			else
				block = Expression.Call(instanceCast, method, valueCast);


			var exp = Expression.Lambda<Action<object, object>>(block, s_ParameterExpressions1).Compile();
			return (instance, value) =>
			{
				CheckParameters(declaringType, propertyType, instance, value);
				exp(instance, value);
			};
		}

		public static Action<object, object, object> BuildCall2Expression(this MethodInfo method)
		{
			var parameters = method.GetParameters();
			Type declaringType;
			Type propertyType;
			Type keyType;
			if (method.IsStatic)
			{
				if (parameters.Length != 3)
					throw new ArgumentOutOfRangeException(nameof(method), "Static method must have three parameters");
				declaringType = parameters[0].ParameterType;
				keyType = parameters[1].ParameterType;
				propertyType = parameters[2].ParameterType;
			}
			else
			{
				if (parameters.Length != 2)
					throw new ArgumentOutOfRangeException(nameof(method), "Instance method must have two parameters");
				declaringType = method.DeclaringType;
				keyType = parameters[0].ParameterType;
				propertyType = parameters[1].ParameterType;
			}

			// value as T is slightly faster than (T)value, so if it's not a value type, use that
			var instanceCast = !declaringType.GetTypeInfo().IsValueType
				? Expression.TypeAs(s_InstanceExpression, declaringType)
				: Expression.Convert(s_InstanceExpression, declaringType);

			var keyCast = !keyType.GetTypeInfo().IsValueType
				? Expression.TypeAs(s_KeyExpression, keyType)
				: Expression.Convert(s_KeyExpression, keyType);

			var valueCast = !propertyType.GetTypeInfo().IsValueType
				? Expression.TypeAs(s_ValueExpression, propertyType)
				: Expression.Convert(s_ValueExpression, propertyType);

			Expression block;
			if (method.IsStatic)
				block = Expression.Call(method, instanceCast, keyCast, valueCast);
			else
				block = Expression.Call(instanceCast, method, keyCast, valueCast);

			var exp = Expression.Lambda<Action<object, object, object>>(block, s_ParameterExpressions2).Compile();

			return (instance, key, value) =>
			{
				CheckParameters(declaringType, keyType, propertyType, instance, key, value);
				exp(instance, key, value);
			};
		}
	}
}
