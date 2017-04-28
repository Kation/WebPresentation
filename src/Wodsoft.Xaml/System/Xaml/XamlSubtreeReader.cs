//using System;
//using System.Collections.Generic;

//namespace System.Xaml
//{
//	internal class XamlSubtreeReader : XamlReader
//	{
//		internal XamlSubtreeReader (XamlReader source)
//		{
//			this.source = source;
//		}
		
//		XamlReader source;

//		public override bool IsEof {
//			get { return started && (nest == 0 || source.IsEof); }
//		}
//		public override XamlMember Member {
//			get { return started ? source.Member : null; }
//		}
		
//		public override NamespaceDeclaration Namespace {
//			get { return started ? source.Namespace : null; }
//		}
		
//		public override XamlNodeType NodeType {
//			get { return started ? source.NodeType : XamlNodeType.None; }
//		}
		
//		public override XamlSchemaContext SchemaContext {
//			get { return source.SchemaContext; }
//		}
		
//		public override XamlType Type {
//			get { return started ? source.Type : null; }
//		}
		
//		public override object Value {
//			get { return started ? source.Value : null; }
//		}
		
//		protected override void Dispose (bool disposing)
//		{
//			while (nest > 0)
//				if (!Read ())
//					break;
//			base.Dispose (disposing);
//		}
		
//		bool started;
//		int nest;
		
//		public override bool Read ()
//		{
//			if (started) {
//				if (nest == 0) {
//					source.Read ();
//					return false; // already consumed
//				}
//				if (!source.Read ())
//					return false;
//			}
//			else
//				started = true;

//			switch (source.NodeType) {
//			case XamlNodeType.StartObject:
//			case XamlNodeType.GetObject:
//			case XamlNodeType.StartMember:
//				nest++;
//				break;
//			case XamlNodeType.EndObject:
//			case XamlNodeType.EndMember:
//				nest--;
//				break;
//			}
//			return true;
//		}
//	}
//}
