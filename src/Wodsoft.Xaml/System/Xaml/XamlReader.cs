using System;
using System.Collections.Generic;

namespace System.Xaml
{
    /// <summary>
    /// Xaml��ȡ����
    /// </summary>
    public abstract class XamlReader : IDisposable
    {
        /// <summary>
        /// ��ȡ�Ƿ����ͷš�
        /// </summary>
        protected bool IsDisposed { get; private set; }

        /// <summary>
        /// ��ȡ�Ƿ��ѽ�β��
        /// </summary>
        public abstract bool IsEof { get; }
        /// <summary>
        /// ��ȡ��ǰXaml��Ա��
        /// </summary>
        public abstract XamlMember Member { get; }
        /// <summary>
        /// ��ȡ��ǰ�����ռ䡣
        /// </summary>
        public abstract NamespaceDeclaration Namespace { get; }
        /// <summary>
        /// ��ȡ��ǰ�ڵ����͡�
        /// </summary>
        public abstract XamlNodeType NodeType { get; }
        /// <summary>
        /// ��ȡXaml�ṹ�����ġ�
        /// </summary>
        public abstract XamlSchemaContext SchemaContext { get; }
        /// <summary>
        /// ��ȡ��ǰXaml���͡�
        /// </summary>
        public abstract XamlType Type { get; }
        /// <summary>
        /// ��ȡ��ǰֵ��
        /// </summary>
        public abstract object Value { get; }

        /// <summary>
        /// �رն�ȡ����
        /// </summary>
        public void Close()
        {
            Dispose(true);
        }

        /// <summary>
        /// �ͷŶ�ȡ����
        /// </summary>
        /// <param name="disposing">�Ƿ����ͷš�</param>
        protected virtual void Dispose(bool disposing)
        {
            IsDisposed = true;
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// ��ȡ��һ�ڵ㡣
        /// </summary>
        /// <returns></returns>
        public abstract bool Read();

        /// <summary>
        /// �ӵ�ǰ�ڵ㴴����ȡ��������
        /// </summary>
        /// <returns></returns>
        public virtual XamlReader ReadSubtree()
        {
            throw new NotImplementedException();
            //return new XamlSubtreeReader(this);
        }

        /// <summary>
        /// ������ǰ�ڵ㡣
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
