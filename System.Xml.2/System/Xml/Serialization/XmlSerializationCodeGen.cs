using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x020001A8 RID: 424
	internal class XmlSerializationCodeGen
	{
		// Token: 0x06001C29 RID: 7209 RVA: 0x00083C4C File Offset: 0x00081E4C
		internal XmlSerializationCodeGen(IndentedWriter writer, TypeScope[] scopes, string access, string className)
		{
			this.writer = writer;
			this.scopes = scopes;
			if (scopes.Length != 0)
			{
				this.stringTypeDesc = scopes[0].GetTypeDesc(typeof(string));
				this.qnameTypeDesc = scopes[0].GetTypeDesc(typeof(XmlQualifiedName));
			}
			this.raCodeGen = new ReflectionAwareCodeGen(writer);
			this.className = className;
			this.access = access;
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001C2A RID: 7210 RVA: 0x00083CD2 File Offset: 0x00081ED2
		internal IndentedWriter Writer
		{
			get
			{
				return this.writer;
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001C2B RID: 7211 RVA: 0x00083CDA File Offset: 0x00081EDA
		// (set) Token: 0x06001C2C RID: 7212 RVA: 0x00083CE2 File Offset: 0x00081EE2
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

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06001C2D RID: 7213 RVA: 0x00083CEB File Offset: 0x00081EEB
		internal ReflectionAwareCodeGen RaCodeGen
		{
			get
			{
				return this.raCodeGen;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001C2E RID: 7214 RVA: 0x00083CF3 File Offset: 0x00081EF3
		internal TypeDesc StringTypeDesc
		{
			get
			{
				return this.stringTypeDesc;
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001C2F RID: 7215 RVA: 0x00083CFB File Offset: 0x00081EFB
		internal TypeDesc QnameTypeDesc
		{
			get
			{
				return this.qnameTypeDesc;
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001C30 RID: 7216 RVA: 0x00083D03 File Offset: 0x00081F03
		internal string ClassName
		{
			get
			{
				return this.className;
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001C31 RID: 7217 RVA: 0x00083D0B File Offset: 0x00081F0B
		internal string Access
		{
			get
			{
				return this.access;
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001C32 RID: 7218 RVA: 0x00083D13 File Offset: 0x00081F13
		internal TypeScope[] Scopes
		{
			get
			{
				return this.scopes;
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001C33 RID: 7219 RVA: 0x00083D1B File Offset: 0x00081F1B
		internal Hashtable MethodNames
		{
			get
			{
				return this.methodNames;
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001C34 RID: 7220 RVA: 0x00083D23 File Offset: 0x00081F23
		internal Hashtable GeneratedMethods
		{
			get
			{
				return this.generatedMethods;
			}
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x00083D2B File Offset: 0x00081F2B
		internal virtual void GenerateMethod(TypeMapping mapping)
		{
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x00083D30 File Offset: 0x00081F30
		internal void GenerateReferencedMethods()
		{
			while (this.references > 0)
			{
				TypeMapping[] array = this.referencedMethods;
				int num = this.references - 1;
				this.references = num;
				TypeMapping mapping = array[num];
				this.GenerateMethod(mapping);
			}
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x00083D68 File Offset: 0x00081F68
		internal string ReferenceMapping(TypeMapping mapping)
		{
			if (!mapping.IsSoap && this.generatedMethods[mapping] == null)
			{
				this.referencedMethods = this.EnsureArrayIndex(this.referencedMethods, this.references);
				TypeMapping[] array = this.referencedMethods;
				int num = this.references;
				this.references = num + 1;
				array[num] = mapping;
			}
			return (string)this.methodNames[mapping];
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x00083DD0 File Offset: 0x00081FD0
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

		// Token: 0x06001C39 RID: 7225 RVA: 0x00083E05 File Offset: 0x00082005
		internal void WriteQuotedCSharpString(string value)
		{
			this.raCodeGen.WriteQuotedCSharpString(value);
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x00083E14 File Offset: 0x00082014
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
			IndentedWriter indentedWriter = this.writer;
			int indent = indentedWriter.Indent;
			indentedWriter.Indent = indent + 1;
			this.writer.WriteLine("get {");
			IndentedWriter indentedWriter2 = this.writer;
			indent = indentedWriter2.Indent;
			indentedWriter2.Indent = indent + 1;
			this.writer.Write("if (");
			this.writer.Write(privateName);
			this.writer.WriteLine(" == null) {");
			IndentedWriter indentedWriter3 = this.writer;
			indent = indentedWriter3.Indent;
			indentedWriter3.Indent = indent + 1;
			this.writer.Write(typeof(Hashtable).FullName);
			this.writer.Write(" _tmp = new ");
			this.writer.Write(typeof(Hashtable).FullName);
			this.writer.WriteLine("();");
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x00083F8C File Offset: 0x0008218C
		internal void GenerateHashtableGetEnd(string privateName)
		{
			this.writer.Write("if (");
			this.writer.Write(privateName);
			this.writer.Write(" == null) ");
			this.writer.Write(privateName);
			this.writer.WriteLine(" = _tmp;");
			IndentedWriter indentedWriter = this.writer;
			int indent = indentedWriter.Indent;
			indentedWriter.Indent = indent - 1;
			this.writer.WriteLine("}");
			this.writer.Write("return ");
			this.writer.Write(privateName);
			this.writer.WriteLine(";");
			IndentedWriter indentedWriter2 = this.writer;
			indent = indentedWriter2.Indent;
			indentedWriter2.Indent = indent - 1;
			this.writer.WriteLine("}");
			IndentedWriter indentedWriter3 = this.writer;
			indent = indentedWriter3.Indent;
			indentedWriter3.Indent = indent - 1;
			this.writer.WriteLine("}");
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x0008407C File Offset: 0x0008227C
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

		// Token: 0x06001C3D RID: 7229 RVA: 0x00084108 File Offset: 0x00082308
		internal void GenerateSupportedTypes(Type[] types)
		{
			this.writer.Write("public override ");
			this.writer.Write(typeof(bool).FullName);
			this.writer.Write(" CanSerialize(");
			this.writer.Write(typeof(Type).FullName);
			this.writer.WriteLine(" type) {");
			IndentedWriter indentedWriter = this.writer;
			int indent = indentedWriter.Indent;
			indentedWriter.Indent = indent + 1;
			Hashtable hashtable = new Hashtable();
			foreach (Type type in types)
			{
				if (!(type == null) && (type.IsPublic || type.IsNestedPublic) && hashtable[type] == null && !DynamicAssemblies.IsTypeDynamic(type) && !type.IsGenericType && (!type.ContainsGenericParameters || !DynamicAssemblies.IsTypeDynamic(type.GetGenericArguments())))
				{
					hashtable[type] = type;
					this.writer.Write("if (type == typeof(");
					this.writer.Write(CodeIdentifier.GetCSharpName(type));
					this.writer.WriteLine(")) return true;");
				}
			}
			this.writer.WriteLine("return false;");
			IndentedWriter indentedWriter2 = this.writer;
			indent = indentedWriter2.Indent;
			indentedWriter2.Indent = indent - 1;
			this.writer.WriteLine("}");
		}

		// Token: 0x06001C3E RID: 7230 RVA: 0x00084264 File Offset: 0x00082464
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
			IndentedWriter indentedWriter = this.writer;
			int indent = indentedWriter.Indent;
			indentedWriter.Indent = indent + 1;
			this.writer.Write("protected override ");
			this.writer.Write(typeof(XmlSerializationReader).FullName);
			this.writer.WriteLine(" CreateReader() {");
			IndentedWriter indentedWriter2 = this.writer;
			indent = indentedWriter2.Indent;
			indentedWriter2.Indent = indent + 1;
			this.writer.Write("return new ");
			this.writer.Write(readerClass);
			this.writer.WriteLine("();");
			IndentedWriter indentedWriter3 = this.writer;
			indent = indentedWriter3.Indent;
			indentedWriter3.Indent = indent - 1;
			this.writer.WriteLine("}");
			this.writer.Write("protected override ");
			this.writer.Write(typeof(XmlSerializationWriter).FullName);
			this.writer.WriteLine(" CreateWriter() {");
			IndentedWriter indentedWriter4 = this.writer;
			indent = indentedWriter4.Indent;
			indentedWriter4.Indent = indent + 1;
			this.writer.Write("return new ");
			this.writer.Write(writerClass);
			this.writer.WriteLine("();");
			IndentedWriter indentedWriter5 = this.writer;
			indent = indentedWriter5.Indent;
			indentedWriter5.Indent = indent - 1;
			this.writer.WriteLine("}");
			IndentedWriter indentedWriter6 = this.writer;
			indent = indentedWriter6.Indent;
			indentedWriter6.Indent = indent - 1;
			this.writer.WriteLine("}");
			return baseSerializer;
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x00084468 File Offset: 0x00082668
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
			IndentedWriter indentedWriter = this.writer;
			int indent = indentedWriter.Indent;
			indentedWriter.Indent = indent + 1;
			this.writer.WriteLine();
			this.writer.Write("public override ");
			this.writer.Write(typeof(bool).FullName);
			this.writer.Write(" CanDeserialize(");
			this.writer.Write(typeof(XmlReader).FullName);
			this.writer.WriteLine(" xmlReader) {");
			IndentedWriter indentedWriter2 = this.writer;
			indent = indentedWriter2.Indent;
			indentedWriter2.Indent = indent + 1;
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
			IndentedWriter indentedWriter3 = this.writer;
			indent = indentedWriter3.Indent;
			indentedWriter3.Indent = indent - 1;
			this.writer.WriteLine("}");
			if (writeMethod != null)
			{
				this.writer.WriteLine();
				this.writer.Write("protected override void Serialize(object objectToSerialize, ");
				this.writer.Write(typeof(XmlSerializationWriter).FullName);
				this.writer.WriteLine(" writer) {");
				IndentedWriter indentedWriter4 = this.writer;
				indent = indentedWriter4.Indent;
				indentedWriter4.Indent = indent + 1;
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
				IndentedWriter indentedWriter5 = this.writer;
				indent = indentedWriter5.Indent;
				indentedWriter5.Indent = indent - 1;
				this.writer.WriteLine("}");
			}
			if (readMethod != null)
			{
				this.writer.WriteLine();
				this.writer.Write("protected override object Deserialize(");
				this.writer.Write(typeof(XmlSerializationReader).FullName);
				this.writer.WriteLine(" reader) {");
				IndentedWriter indentedWriter6 = this.writer;
				indent = indentedWriter6.Indent;
				indentedWriter6.Indent = indent + 1;
				this.writer.Write("return ((");
				this.writer.Write(readerClass);
				this.writer.Write(")reader).");
				this.writer.Write(readMethod);
				this.writer.WriteLine("();");
				IndentedWriter indentedWriter7 = this.writer;
				indent = indentedWriter7.Indent;
				indentedWriter7.Indent = indent - 1;
				this.writer.WriteLine("}");
			}
			IndentedWriter indentedWriter8 = this.writer;
			indent = indentedWriter8.Indent;
			indentedWriter8.Indent = indent - 1;
			this.writer.WriteLine("}");
			return text;
		}

		// Token: 0x06001C40 RID: 7232 RVA: 0x0008481C File Offset: 0x00082A1C
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

		// Token: 0x06001C41 RID: 7233 RVA: 0x000848DC File Offset: 0x00082ADC
		private void GenerateGetSerializer(Hashtable serializers, XmlMapping[] xmlMappings)
		{
			this.writer.Write("public override ");
			this.writer.Write(typeof(XmlSerializer).FullName);
			this.writer.Write(" GetSerializer(");
			this.writer.Write(typeof(Type).FullName);
			this.writer.WriteLine(" type) {");
			IndentedWriter indentedWriter = this.writer;
			int indent = indentedWriter.Indent;
			indentedWriter.Indent = indent + 1;
			for (int i = 0; i < xmlMappings.Length; i++)
			{
				if (xmlMappings[i] is XmlTypeMapping)
				{
					Type type = xmlMappings[i].Accessor.Mapping.TypeDesc.Type;
					if (!(type == null) && (type.IsPublic || type.IsNestedPublic) && !DynamicAssemblies.IsTypeDynamic(type) && !type.IsGenericType && (!type.ContainsGenericParameters || !DynamicAssemblies.IsTypeDynamic(type.GetGenericArguments())))
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
			IndentedWriter indentedWriter2 = this.writer;
			indent = indentedWriter2.Indent;
			indentedWriter2.Indent = indent - 1;
			this.writer.WriteLine("}");
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x00084A74 File Offset: 0x00082C74
		internal void GenerateSerializerContract(string className, XmlMapping[] xmlMappings, Type[] types, string readerType, string[] readMethods, string writerType, string[] writerMethods, Hashtable serializers)
		{
			this.writer.WriteLine();
			this.writer.Write("public class XmlSerializerContract : global::");
			this.writer.Write(typeof(XmlSerializerImplementation).FullName);
			this.writer.WriteLine(" {");
			IndentedWriter indentedWriter = this.writer;
			int indent = indentedWriter.Indent;
			indentedWriter.Indent = indent + 1;
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
			IndentedWriter indentedWriter2 = this.writer;
			indent = indentedWriter2.Indent;
			indentedWriter2.Indent = indent - 1;
			this.writer.WriteLine("}");
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x00084BEC File Offset: 0x00082DEC
		internal static bool IsWildcard(SpecialMapping mapping)
		{
			if (mapping is SerializableMapping)
			{
				return ((SerializableMapping)mapping).IsAny;
			}
			return mapping.TypeDesc.CanBeElementValue;
		}

		// Token: 0x04000C40 RID: 3136
		private IndentedWriter writer;

		// Token: 0x04000C41 RID: 3137
		private int nextMethodNumber;

		// Token: 0x04000C42 RID: 3138
		private Hashtable methodNames = new Hashtable();

		// Token: 0x04000C43 RID: 3139
		private ReflectionAwareCodeGen raCodeGen;

		// Token: 0x04000C44 RID: 3140
		private TypeScope[] scopes;

		// Token: 0x04000C45 RID: 3141
		private TypeDesc stringTypeDesc;

		// Token: 0x04000C46 RID: 3142
		private TypeDesc qnameTypeDesc;

		// Token: 0x04000C47 RID: 3143
		private string access;

		// Token: 0x04000C48 RID: 3144
		private string className;

		// Token: 0x04000C49 RID: 3145
		private TypeMapping[] referencedMethods;

		// Token: 0x04000C4A RID: 3146
		private int references;

		// Token: 0x04000C4B RID: 3147
		private Hashtable generatedMethods = new Hashtable();
	}
}
