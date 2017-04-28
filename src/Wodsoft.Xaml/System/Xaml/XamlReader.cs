using System;
using System.Collections.Generic;

namespace System.Xaml
{
    /// <summary>
    /// Xaml读取器。
    /// </summary>
    public abstract class XamlReader : IDisposable
    {
        /// <summary>
        /// 获取是否已释放。
        /// </summary>
        protected bool IsDisposed { get; private set; }

        /// <summary>
        /// 获取是否已结尾。
        /// </summary>
        public abstract bool IsEof { get; }
        /// <summary>
        /// 获取当前Xaml成员。
        /// </summary>
        public abstract XamlMember Member { get; }
        /// <summary>
        /// 获取当前命名空间。
        /// </summary>
        public abstract NamespaceDeclaration Namespace { get; }
        /// <summary>
        /// 获取当前节点类型。
        /// </summary>
        public abstract XamlNodeType NodeType { get; }
        /// <summary>
        /// 获取Xaml结构上下文。
        /// </summary>
        public abstract XamlSchemaContext SchemaContext { get; }
        /// <summary>
        /// 获取当前Xaml类型。
        /// </summary>
        public abstract XamlType Type { get; }
        /// <summary>
        /// 获取当前值。
        /// </summary>
        public abstract object Value { get; }

        /// <summary>
        /// 关闭读取器。
        /// </summary>
        public void Close()
        {
            Dispose(true);
        }

        /// <summary>
        /// 释放读取器。
        /// </summary>
        /// <param name="disposing">是否已释放。</param>
        protected virtual void Dispose(bool disposing)
        {
            IsDisposed = true;
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// 读取下一节点。
        /// </summary>
        /// <returns></returns>
        public abstract bool Read();

        /// <summary>
        /// 从当前节点创建读取器副本。
        /// </summary>
        /// <returns></returns>
        public virtual XamlReader ReadSubtree()
        {
            throw new NotImplementedException();
            //return new XamlSubtreeReader(this);
        }

        /// <summary>
        /// 跳过当前节点。
        /// </summary>
        public virtual void Skip()
        {
            int count = 0;
            switch (NodeType)
            {
                case XamlNodeType.StartMember:
                case XamlNodeType.StartObject:
                case XamlNodeType.GetObject:
                    count++;
                    while (Read())
                    {
                        switch (NodeType)
                        {
                            case XamlNodeType.StartMember:
                            case XamlNodeType.GetObject:
                            case XamlNodeType.StartObject:
                                count++;
                                continue;
                            case XamlNodeType.EndMember:
                            case XamlNodeType.EndObject:
                                count--;
                                if (count == 0)
                                {
                                    Read();
                                    return;
                                }
                                continue;
                        }
                    }
                    return;
                default:
                    Read();
                    return;
            }
        }
    }
}
