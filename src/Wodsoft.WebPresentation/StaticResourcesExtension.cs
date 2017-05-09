using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Xaml;

namespace Wodsoft.Web
{
    public class StaticResourcesExtension : MarkupExtension
    {
        public StaticResourcesExtension() { }

        public StaticResourcesExtension(object resourceKey)
        {
            if (resourceKey == null)
                throw new ArgumentNullException(nameof(resourceKey));
            ResourceKey = resourceKey;
        }

        [ConstructorArgument("resourceKey")]
        public object ResourceKey { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var res = GetResourceDictionary(serviceProvider);
            if (res == null)
                return null;
            return res[ResourceKey];
        }

        private ResourceDictionary GetResourceDictionary(IServiceProvider serviceProvider)
        {
            IXamlSchemaContextProvider xamlSchemaContextProvider = serviceProvider.GetService(typeof(IXamlSchemaContextProvider)) as IXamlSchemaContextProvider;
            if (xamlSchemaContextProvider == null)
                throw new InvalidOperationException("找不到XamlSchema上下文提供器。");
            IAmbientProvider ambientProvider = serviceProvider.GetService(typeof(IAmbientProvider)) as IAmbientProvider;
            if (ambientProvider == null)
                throw new InvalidOperationException("找不到环境提供器。");
            XamlSchemaContext schemaContext = xamlSchemaContextProvider.SchemaContext;
            XamlType xamlType = schemaContext.GetXamlType(typeof(FrameworkElement));
            XamlType xamlType2 = schemaContext.GetXamlType(typeof(Style));
            XamlType xamlType3 = schemaContext.GetXamlType(typeof(FrameworkTemplate));
            XamlType xamlType4 = schemaContext.GetXamlType(typeof(Application));
            XamlMember member2 = xamlType.GetMember("Resources");
            XamlMember member3 = xamlType2.GetMember("Resources");
            XamlMember member4 = xamlType2.GetMember("BasedOn");
            XamlMember member5 = xamlType3.GetMember("Resources");
            XamlMember member6 = xamlType4.GetMember("Resources");
            XamlType[] types = new XamlType[]
            {
                schemaContext.GetXamlType(typeof(ResourceDictionary))
            };
            IEnumerable<AmbientPropertyValue> allAmbientValues = ambientProvider.GetAllAmbientValues(null, false, types, new XamlMember[]
            {
                member2,
                member3,
                member4,
                member5,
                member6
            });
            List<AmbientPropertyValue> list = allAmbientValues as List<AmbientPropertyValue>;
            for (int i = 0; i < list.Count; i++)
            {
                AmbientPropertyValue ambientPropertyValue = list[i];
                if (ambientPropertyValue.Value is ResourceDictionary)
                {
                    ResourceDictionary resourceDictionary = (ResourceDictionary)ambientPropertyValue.Value;
                    if (resourceDictionary.Contains(ResourceKey))
                    {
                        return resourceDictionary;
                    }
                }
                if (ambientPropertyValue.Value is Style)
                {
                    Style style = (Style)ambientPropertyValue.Value;
                    ResourceDictionary resourceDictionary2 = style.FindResourceDictionary(ResourceKey);
                    if (resourceDictionary2 != null)
                    {
                        return resourceDictionary2;
                    }
                }
            }
            return null;
        }
    }
}
