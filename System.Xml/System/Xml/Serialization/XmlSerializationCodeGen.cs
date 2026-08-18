using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x02000324 RID: 804
	internal class XmlSerializationCodeGen
	{
		// Token: 0x0600265D RID: 9821 RVA: 0x000BAF9C File Offset: 0x000B9F9C
		internal XmlSerializationCodeGen(IndentedWriter writer, TypeScope[] scopes, string access, string className)
		{
			this.writer = writer;
			this.scopes = scopes;
			if (scopes.Length > 0)
			{
				this.stringTypeDesc = scopes[0].GetTypeDesc(typeof(string));
				this.qnameTypeDesc = scopes[0].GetTypeDesc(typeof(XmlQualifiedName));
			}
			this.raCodeGen = new ReflectionAwareCodeGen(writer);
			this.className = className;
			this.access = access;
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x0600265E RID: 9822 RVA: 0x000BB024 File Offset: 0x000BA024
		internal IndentedWriter Writer
		{
			get
			{
				return this.writer;
			}
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x0600265F RID: 9823 RVA: 0x000BB02C File Offset: 0x000BA02C
		// (set) Token: 0x06002660 RID: 9824 RVA: 0x000BB034 File Offset: 0x000BA034
		internal int NextMethodNumber
		{
			get
			{
				return this.nextMethodNumber;
			}
			set
			{
				this.nextMethodNumber = value;
			}
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06002661 RID: 9825 RVA: 0x000BB03D File Offset: 0x000BA03D
		internal ReflectionAwareCodeGen RaCodeGen
		{
			get
			{
				return this.raCodeGen;
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06002662 RID: 9826 RVA: 0x000BB045 File Offset: 0x000BA045
		internal TypeDesc StringTypeDesc
		{
			get
			{
				return this.stringTypeDesc;
			}
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06002663 RID: 9827 RVA: 0x000BB04D File Offset: 0x000BA04D
		internal TypeDesc QnameTypeDesc
		{
			get
			{
				return this.qnameTypeDesc;
			}
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06002664 RID: 9828 RVA: 0x000BB055 File Offset: 0x000BA055
		internal string ClassName
		{
			get
			{
				return this.className;
			}
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06002665 RID: 9829 RVA: 0x000BB05D File Offset: 0x000BA05D
		internal string Access
		{
			get
			{
				return this.access;
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06002666 RID: 9830 RVA: 0x000BB065 File Offset: 0x000BA065
		internal TypeScope[] Scopes
		{
			get
			{
				return this.scopes;
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06002667 RID: 9831 RVA: 0x000BB06D File Offset: 0x000BA06D
		internal Hashtable MethodNames
		{
			get
			{
				return this.methodNames;
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06002668 RID: 9832 RVA: 0x000BB075 File Offset: 0x000BA075
		internal Hashtable GeneratedMethods
		{
			get
			{
				return this.generatedMethods;
			}
		}

		// Token: 0x06002669 RID: 9833 RVA: 0x000BB07D File Offset: 0x000BA07D
		internal virtual void GenerateMethod(TypeMapping mapping)
		{
		}

		// Token: 0x0600266A RID: 9834 RVA: 0x000BB080 File Offset: 0x000BA080
		internal void GenerateReferencedMethods()
		{
			while (this.references > 0)
			{
				TypeMapping mapping = this.referencedMethods[--this.references];
				this.GenerateMethod(mapping);
			}
		}

		// Token: 0x0600266B RID: 9835 RVA: 0x000BB0B8 File Offset: 0x000BA0B8
		internal string ReferenceMapping(TypeMapping mapping)
		{
			if (!mapping.IsSoap && this.generatedMethods[mapping] == null)
			{
				this.referencedMethods = this.EnsureArrayIndex(this.referencedMethods, this.references);
				this.referencedMethods[this.references++] = mapping;
			}
			return (string)this.methodNames[mapping];
		}

		// Token: 0x0600266C RID: 9836 RVA: 0x000BB120 File Offset: 0x000BA120
		private TypeMapping[] EnsureArrayIndex(TypeMapping[] a, int index)
		{
			if (a == null)
			{
				return new TypeMapping[32];
			}
			if (index < a.Length)
			{
				return a;
			}
			TypeMapping[] array = new TypeMapping[a.Length + 32];
			Array.Copy(a, array, index);
			return array;
		}

		// Token: 0x0600266D RID: 9837 RVA: 0x000BB155 File Offset: 0x000BA155
		internal void WriteQuotedCSharpString(string value)
		{
			this.raCodeGen.WriteQuotedCSharpString(value);
		}

		// Token: 0x0600266E RID: 9838 RVA: 0x000BB164 File Offset: 0x000BA164
		internal void GenerateHashtableGetBegin(string privateName, string publicName)
		{
			this.writer.Write(typeof(Hashtable).FullName);
			this.writer.Write(" ");
			this.writer.Write(privateName);
			this.writer.WriteLine(" = null;");
			this.writer.Write("public override ");
			this.writer.Write(typeof(Hashtable).FullName);
			this.writer.Write(" ");
			this.writer.Write(publicName);
			this.writer.WriteLine(" {");
			this.writer.Indent++;
			this.writer.WriteLine("get {");
			this.writer.Indent++;
			this.writer.Write("if (");
			this.writer.Write(privateName);
			this.writer.WriteLine(" == null) {");
			this.writer.Indent++;
			this.writer.Write(typeof(Hashtable).FullName);
			this.writer.Write(" _tmp = new ");
			this.writer.Write(typeof(Hashtable).FullName);
			this.writer.WriteLine("();");
		}

		// Token: 0x0600266F RID: 9839 RVA: 0x000BB2D8 File Offset: 0x000BA2D8
		internal void GenerateHashtableGetEnd(string privateName)
		{
			this.writer.Write("if (");
			this.writer.Write(privateName);
			this.writer.Write(" == null) ");
			this.writer.Write(privateName);
			this.writer.WriteLine(" = _tmp;");
			this.writer.Indent--;
			this.writer.WriteLine("}");
			this.writer.Write("return ");
			this.writer.Write(privateName);
			this.writer.WriteLine(";");
			this.writer.Indent--;
			this.writer.WriteLine("}");
			this.writer.Indent--;
			this.writer.WriteLine("}");
		}

		// Token: 0x06002670 RID: 9840 RVA: 0x000BB3C4 File Offset: 0x000BA3C4
		internal void GeneratePublicMethods(string privateName, string publicName, string[] methods, XmlMapping[] xmlMappings)
		{
			this.GenerateHashtableGetBegin(privateName, publicName);
			if (methods != null && methods.Length != 0 && xmlMappings != null && xmlMappings.Length == methods.Length)
			{
				for (int i = 0; i < methods.Length; i++)
				{
					if (methods[i] != null)
					{
						this.writer.Write("_tmp[");
						this.WriteQuotedCSharpString(xmlMappings[i].Key);
						this.writer.Write("] = ");
						this.WriteQuotedCSharpString(methods[i]);
						this.writer.WriteLine(";");
					}
				}
			}
			this.GenerateHashtableGetEnd(privateName);
		}

		// Token: 0x06002671 RID: 9841 RVA: 0x000BB450 File Offset: 0x000BA450
		internal void GenerateSupportedTypes(Type[] types)
		{
			this.writer.Write("public override ");
			this.writer.Write(typeof(bool).FullName);
			this.writer.Write(" CanSerialize(");
			this.writer.Write(typeof(Type).FullName);
			this.writer.WriteLine(" type) {");
			this.writer.Indent++;
			Hashtable hashtable = new Hashtable();
			foreach (Type type in types)
			{
				if (type != null && (type.IsPublic || type.IsNestedPublic) && hashtable[type] == null && !DynamicAssemblies.IsTypeDynamic(type) && !type.IsGenericType && (!type.ContainsGenericParameters || !DynamicAssemblies.IsTypeDynamic(type.GetGenericArguments())))
				{
					hashtable[type] = type;
					this.writer.Write("if (type == typeof(");
					this.writer.Write(CodeIdentifier.GetCSharpName(type));
					this.writer.WriteLine(")) return true;");
				}
			}
			this.writer.WriteLine("return false;");
			this.writer.Indent--;
			this.writer.WriteLine("}");
		}

		// Token: 0x06002672 RID: 9842 RVA: 0x000BB5A0 File Offset: 0x000BA5A0
		internal string GenerateBaseSerializer(string baseSerializer, string readerClass, string writerClass, CodeIdentifiers classes)
		{
			baseSerializer = CodeIdentifier.MakeValid(baseSerializer);
			baseSerializer = classes.AddUnique(baseSerializer, baseSerializer);
			this.writer.WriteLine();
			this.writer.Write("public abstract class ");
			this.writer.Write(CodeIdentifier.GetCSharpName(baseSerializer));
			this.writer.Write(" : ");
			this.writer.Write(typeof(XmlSerializer).FullName);
			this.writer.WriteLine(" {");
			this.writer.Indent++;
			this.writer.Write("protected override ");
			this.writer.Write(typeof(XmlSerializationReader).FullName);
			this.writer.WriteLine(" CreateReader() {");
			this.writer.Indent++;
			this.writer.Write("return new ");
			this.writer.Write(readerClass);
			this.writer.WriteLine("();");
			this.writer.Indent--;
			this.writer.WriteLine("}");
			this.writer.Write("protected override ");
			this.writer.Write(typeof(XmlSerializationWriter).FullName);
			this.writer.WriteLine(" CreateWriter() {");
			this.writer.Indent++;
			this.writer.Write("return new ");
			this.writer.Write(writerClass);
			this.writer.WriteLine("();");
			this.writer.Indent--;
			this.writer.WriteLine("}");
			this.writer.Indent--;
			this.writer.WriteLine("}");
			return baseSerializer;
		}

		// Token: 0x06002673 RID: 9843 RVA: 0x000BB798 File Offset: 0x000BA798
		internal string GenerateTypedSerializer(string readMethod, string writeMethod, XmlMapping mapping, CodeIdentifiers classes, string baseSerializer, string readerClass, string writerClass)
		{
			string text = CodeIdentifier.MakeValid(Accessor.UnescapeName(mapping.Accessor.Mapping.TypeDesc.Name));
			text = classes.AddUnique(text + "Serializer", mapping);
			this.writer.WriteLine();
			this.writer.Write("public sealed class ");
			this.writer.Write(CodeIdentifier.GetCSharpName(text));
			this.writer.Write(" : ");
			this.writer.Write(baseSerializer);
			this.writer.WriteLine(" {");
			this.writer.Indent++;
			this.writer.WriteLine();
			this.writer.Write("public override ");
			this.writer.Write(typeof(bool).FullName);
			this.writer.Write(" CanDeserialize(");
			this.writer.Write(typeof(XmlReader).FullName);
			this.writer.WriteLine(" xmlReader) {");
			this.writer.Indent++;
			if (mapping.Accessor.Any)
			{
				this.writer.WriteLine("return true;");
			}
			else
			{
				this.writer.Write("return xmlReader.IsStartElement(");
				this.WriteQuotedCSharpString(mapping.Accessor.Name);
				this.writer.Write(", ");
				this.WriteQuotedCSharpString(mapping.Accessor.Namespace);
				this.writer.WriteLine(");");
			}
			this.writer.Indent--;
			this.writer.WriteLine("}");
			if (writeMethod != null)
			{
				this.writer.WriteLine();
				this.writer.Write("protected override void Serialize(object objectToSerialize, ");
				this.writer.Write(typeof(XmlSerializationWriter).FullName);
				this.writer.WriteLine(" writer) {");
				this.writer.Indent++;
				this.writer.Write("((");
				this.writer.Write(writerClass);
				this.writer.Write(")writer).");
				this.writer.Write(writeMethod);
				this.writer.Write("(");
				if (mapping is XmlMembersMapping)
				{
					this.writer.Write("(object[])");
				}
				this.writer.WriteLine("objectToSerialize);");
				this.writer.Indent--;
				this.writer.WriteLine("}");
			}
			if (readMethod != null)
			{
				this.writer.WriteLine();
				this.writer.Write("protected override object Deserialize(");
				this.writer.Write(typeof(XmlSerializationReader).FullName);
				this.writer.WriteLine(" reader) {");
				this.writer.Indent++;
				this.writer.Write("return ((");
				this.writer.Write(readerClass);
				this.writer.Write(")reader).");
				this.writer.Write(readMethod);
				this.writer.WriteLine("();");
				this.writer.Indent--;
				this.writer.WriteLine("}");
			}
			this.writer.Indent--;
			this.writer.WriteLine("}");
			return text;
		}

		// Token: 0x06002674 RID: 9844 RVA: 0x000BBB3C File Offset: 0x000BAB3C
		private void GenerateTypedSerializers(Hashtable serializers)
		{
			string privateName = "typedSerializers";
			this.GenerateHashtableGetBegin(privateName, "TypedSerializers");
			foreach (object obj in serializers.Keys)
			{
				string text = (string)obj;
				this.writer.Write("_tmp.Add(");
				this.WriteQuotedCSharpString(text);
				this.writer.Write(", new ");
				this.writer.Write((string)serializers[text]);
				this.writer.WriteLine("());");
			}
			this.GenerateHashtableGetEnd("typedSerializers");
		}

		// Token: 0x06002675 RID: 9845 RVA: 0x000BBBFC File Offset: 0x000BABFC
		private void GenerateGetSerializer(Hashtable serializers, XmlMapping[] xmlMappings)
		{
			this.writer.Write("public override ");
			this.writer.Write(typeof(XmlSerializer).FullName);
			this.writer.Write(" GetSerializer(");
			this.writer.Write(typeof(Type).FullName);
			this.writer.WriteLine(" type) {");
			this.writer.Indent++;
			for (int i = 0; i < xmlMappings.Length; i++)
			{
				if (xmlMappings[i] is XmlTypeMapping)
				{
					Type type = xmlMappings[i].Accessor.Mapping.TypeDesc.Type;
					if (type != null && (type.IsPublic || type.IsNestedPublic) && !DynamicAssemblies.IsTypeDynamic(type) && !type.IsGenericType && (!type.ContainsGenericParameters || !DynamicAssemblies.IsTypeDynamic(type.GetGenericArguments())))
					{
						this.writer.Write("if (type == typeof(");
						this.writer.Write(CodeIdentifier.GetCSharpName(type));
						this.writer.Write(")) return new ");
						this.writer.Write((string)serializers[xmlMappings[i].Key]);
						this.writer.WriteLine("();");
					}
				}
			}
			this.writer.WriteLine("return null;");
			this.writer.Indent--;
			this.writer.WriteLine("}");
		}

		// Token: 0x06002676 RID: 9846 RVA: 0x000BBD8C File Offset: 0x000BAD8C
		internal void GenerateSerializerContract(string className, XmlMapping[] xmlMappings, Type[] types, string readerType, string[] readMethods, string writerType, string[] writerMethods, Hashtable serializers)
		{
			this.writer.WriteLine();
			this.writer.Write("public class XmlSerializerContract : global::");
			this.writer.Write(typeof(XmlSerializerImplementation).FullName);
			this.writer.WriteLine(" {");
			this.writer.Indent++;
			this.writer.Write("public override global::");
			this.writer.Write(typeof(XmlSerializationReader).FullName);
			this.writer.Write(" Reader { get { return new ");
			this.writer.Write(readerType);
			this.writer.WriteLine("(); } }");
			this.writer.Write("public override global::");
			this.writer.Write(typeof(XmlSerializationWriter).FullName);
			this.writer.Write(" Writer { get { return new ");
			this.writer.Write(writerType);
			this.writer.WriteLine("(); } }");
			this.GeneratePublicMethods("readMethods", "ReadMethods", readMethods, xmlMappings);
			this.GeneratePublicMethods("writeMethods", "WriteMethods", writerMethods, xmlMappings);
			this.GenerateTypedSerializers(serializers);
			this.GenerateSupportedTypes(types);
			this.GenerateGetSerializer(serializers, xmlMappings);
			this.writer.Indent--;
			this.writer.WriteLine("}");
		}

		// Token: 0x06002677 RID: 9847 RVA: 0x000BBF00 File Offset: 0x000BAF00
		internal static bool IsWildcard(SpecialMapping mapping)
		{
			if (mapping is SerializableMapping)
			{
				return ((SerializableMapping)mapping).IsAny;
			}
			return mapping.TypeDesc.CanBeElementValue;
		}

		// Token: 0x040015D4 RID: 5588
		private IndentedWriter writer;

		// Token: 0x040015D5 RID: 5589
		private int nextMethodNumber;

		// Token: 0x040015D6 RID: 5590
		private Hashtable methodNames = new Hashtable();

		// Token: 0x040015D7 RID: 5591
		private ReflectionAwareCodeGen raCodeGen;

		// Token: 0x040015D8 RID: 5592
		private TypeScope[] scopes;

		// Token: 0x040015D9 RID: 5593
		private TypeDesc stringTypeDesc;

		// Token: 0x040015DA RID: 5594
		private TypeDesc qnameTypeDesc;

		// Token: 0x040015DB RID: 5595
		private string access;

		// Token: 0x040015DC RID: 5596
		private string className;

		// Token: 0x040015DD RID: 5597
		private TypeMapping[] referencedMethods;

		// Token: 0x040015DE RID: 5598
		private int references;

		// Token: 0x040015DF RID: 5599
		private Hashtable generatedMethods = new Hashtable();
	}
}
