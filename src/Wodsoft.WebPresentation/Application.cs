using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web
{
    public class Application : IHaveResources
    {
        public Application()
        {
            Resources = new ResourceDictionary();
        }
        
        [Browsable(false)]
        public ResourceDictionary Resources { get; set; }
        
        private FrameworkElement _Root;
        [Browsable(false)]
        public FrameworkElement Root
        {
            get { return _Root; }
            set
            {
                if (_Root != null)
                    _Root.Resources.MergedDictionaries.Remove(Resources);
                _Root = value;
                if (_Root != null)
                    _Root.Resources.MergedDictionaries.Add(Resources);
            }
        }
        
        private IDictionary _Properties;
        [Browsable(false)]
        public IDictionary Properties
        {
            get
            {
                if (_Properties == null)
                    _Properties = new HybridDictionary();
                return _Properties;
            }
        }

        public object FindResource(object key)
        {
            return Resources[key];
        }
    }
}
