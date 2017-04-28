using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Xaml;

namespace Wodsoft.Web
{
    public class DependencyPropertyConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return false;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            string propertyName = value as string;
            if (propertyName == null)
                throw new NotSupportedException();
            if (propertyName.Contains("."))
            {
                string[] dop = propertyName.Split('.');
                if (dop.Length != 2)
                    return null;
                Type ownerType = Type.GetType(dop[0]);
                if (ownerType == null)
                    return null;
                DependencyProperty dp = DependencyProperty.FromName(dop[1], ownerType);
                if (dp == null)
                    throw new NotSupportedException("Can not converter dependency property.");
                return dp;
            }
            IXamlSchemaContextProvider xamlSchemaContextProvider = context.GetService(typeof(IXamlSchemaContextProvider)) as IXamlSchemaContextProvider;
            if (xamlSchemaContextProvider == null)
                throw new NotSupportedException("Can not converter dependency property.");

            IAmbientProvider ambientProvider = context.GetService(typeof(IAmbientProvider)) as IAmbientProvider;
            if (ambientProvider == null)
                throw new NotSupportedException("Can not converter dependency property.");

            XamlSchemaContext schemaContext = xamlSchemaContextProvider.SchemaContext;
            List<XamlType> list = new List<XamlType>();
            list.Add(schemaContext.GetXamlType(typeof(Style)));
            AmbientPropertyValue firstAmbientValue = ambientProvider.GetFirstAmbientValue(list, list[0].GetMember("TargetType"));
            if (firstAmbientValue == null)
                throw new NotSupportedException("Can not converter dependency property.");
            Type type = (Type)firstAmbientValue.Value;
            return DependencyProperty.FromName(propertyName, type.GetProperty(propertyName).DeclaringType);
        }
    }
}
