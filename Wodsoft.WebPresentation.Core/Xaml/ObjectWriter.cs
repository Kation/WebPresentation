using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;

namespace Wodsoft.Web.Xaml
{
    public class ObjectWriter : XamlObjectWriter
    {
        public ObjectWriter() : base(new XamlSchemaContext()) { }

        public ObjectWriter(XamlSchemaContext schemaContext) : base(schemaContext) { }

        object _Instance;
        protected override void OnBeforeProperties(object value)
        {
            _Instance = value;
            base.OnBeforeProperties(value);
        }

        //设置属性值
        protected override bool OnSetValue(object eventSender, XamlMember member, object value)
        {
            if (eventSender is DependencyObject)
            {
                //获取依赖属性
                DependencyProperty dp = DependencyProperty.FromName(member.Name, member.DeclaringType.UnderlyingType);
                if (dp == null)
                {
                    //如果不是依赖属性，则使用CLR方法赋值
                    return base.OnSetValue(eventSender, member, value);
                }
                DependencyObject target = (DependencyObject)eventSender;
                //使用自己框架的SetValue方法赋值
                if (value != null && !(value is Expression) && member.Type.UnderlyingType != typeof(object) && member.TypeConverter != null && !member.Type.UnderlyingType.IsAssignableFrom(value.GetType()))
                    value = member.TypeConverter.ConverterInstance.ConvertFrom(value);
                target.SetValue(dp, value);
                return true;
            }
            else
            {
                if (value != null && member.Type.UnderlyingType != typeof(object) && member.TypeConverter != null && !member.Type.UnderlyingType.IsAssignableFrom(value.GetType()))
                    value = member.TypeConverter.ConverterInstance.ConvertFrom(value);
                return base.OnSetValue(eventSender, member, value);
            }
        }

        //写入成员方法
        public override void WriteStartMember(XamlMember property)
        {
            //判断是否是依赖类型
            if (property.DeclaringType != null && property.DeclaringType.UnderlyingType.IsSubclassOf(typeof(DependencyObject)))
            {
                //如果是属性
                if (property.UnderlyingMember is PropertyInfo)
                {
                    ////防止目标类型未调用静态构造函数
                    ////这里我不知道还有什么方法可以引发类型的静态构造函数
                    //if (_Instance == null)
                    //    Activator.CreateInstance(property.DeclaringType.UnderlyingType);
                    //获取依赖属性
                    DependencyProperty dp = DependencyProperty.FromName(property.Name, property.DeclaringType.UnderlyingType);
                    if (dp != null)
                    {
                        //如果是依赖属性
                        //覆盖XamlMember
                        //使用我们自己MemberInvoker
                        property = new XamlMember((PropertyInfo)property.UnderlyingMember, SchemaContext, new ObjectMemberInvoker(dp));
                    }
                }
            }
            base.WriteStartMember(property);
        }
    }
}
