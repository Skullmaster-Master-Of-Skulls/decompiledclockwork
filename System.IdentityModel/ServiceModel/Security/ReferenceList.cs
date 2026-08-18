using System;
using System.Collections.Generic;
using System.IdentityModel;
using System.Runtime.CompilerServices;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x0200000E RID: 14
	[TypeForwardedFrom("System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	internal sealed class ReferenceList : ISecurityElement
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002CFC File Offset: 0x00000EFC
		public int DataReferenceCount
		{
			get
			{
				return this.referredIds.Count;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002D09 File Offset: 0x00000F09
		public bool HasId
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002D0C File Offset: 0x00000F0C
		public string Id
		{
			get
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002D1D File Offset: 0x00000F1D
		public void AddReferredId(string id)
		{
			if (id == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("id"));
			}
			this.referredIds.Add(id);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002D43 File Offset: 0x00000F43
		public bool ContainsReferredId(string id)
		{
			if (id == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("id"));
			}
			return this.referredIds.Contains(id);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002D69 File Offset: 0x00000F69
		public string GetReferredId(int index)
		{
			return this.referredIds[index];
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002D78 File Offset: 0x00000F78
		public void ReadFrom(XmlDictionaryReader reader)
		{
			reader.ReadStartElement(ReferenceList.ElementName, ReferenceList.NamespaceUri);
			while (reader.IsStartElement())
			{
				string text = ReferenceList.DataReference.ReadFrom(reader);
				if (this.referredIds.Contains(text))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityMessageSerializationException(SR.GetString("InvalidDataReferenceInReferenceList", new object[]
					{
						"#" + text
					})));
				}
				this.referredIds.Add(text);
			}
			reader.ReadEndElement();
			if (this.DataReferenceCount == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityMessageSerializationException(SR.GetString("ReferenceListCannotBeEmpty")));
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002E16 File Offset: 0x00001016
		public bool TryRemoveReferredId(string id)
		{
			if (id == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("id"));
			}
			return this.referredIds.Remove(id);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002E3C File Offset: 0x0000103C
		public void WriteTo(XmlDictionaryWriter writer, DictionaryManager dictionaryManager)
		{
			if (this.DataReferenceCount == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ReferenceListCannotBeEmpty")));
			}
			writer.WriteStartElement("e", ReferenceList.ElementName, ReferenceList.NamespaceUri);
			for (int i = 0; i < this.DataReferenceCount; i++)
			{
				ReferenceList.DataReference.WriteTo(writer, this.referredIds[i]);
			}
			writer.WriteEndElement();
		}

		// Token: 0x0400006E RID: 110
		internal static readonly XmlDictionaryString ElementName = XD.XmlEncryptionDictionary.ReferenceList;

		// Token: 0x0400006F RID: 111
		private const string NamespacePrefix = "e";

		// Token: 0x04000070 RID: 112
		internal static readonly XmlDictionaryString NamespaceUri = EncryptedType.NamespaceUri;

		// Token: 0x04000071 RID: 113
		internal static readonly XmlDictionaryString UriAttribute = XD.XmlEncryptionDictionary.URI;

		// Token: 0x04000072 RID: 114
		private List<string> referredIds = new List<string>();

		// Token: 0x0200021C RID: 540
		private static class DataReference
		{
			// Token: 0x060011C6 RID: 4550 RVA: 0x0004E22C File Offset: 0x0004C42C
			public static string ReadFrom(XmlDictionaryReader reader)
			{
				string text2;
				string text = XmlHelper.ReadEmptyElementAndRequiredAttribute(reader, ReferenceList.DataReference.ElementName, ReferenceList.DataReference.NamespaceUri, ReferenceList.UriAttribute, out text2);
				if (text.Length < 2 || text[0] != '#')
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityMessageSerializationException(SR.GetString("InvalidDataReferenceInReferenceList", new object[]
					{
						text
					})));
				}
				return text.Substring(1);
			}

			// Token: 0x060011C7 RID: 4551 RVA: 0x0004E290 File Offset: 0x0004C490
			public static void WriteTo(XmlDictionaryWriter writer, string referredId)
			{
				writer.WriteStartElement(XD.XmlEncryptionDictionary.Prefix.Value, ReferenceList.DataReference.ElementName, ReferenceList.DataReference.NamespaceUri);
				writer.WriteStartAttribute(ReferenceList.UriAttribute, null);
				writer.WriteString("#");
				writer.WriteString(referredId);
				writer.WriteEndAttribute();
				writer.WriteEndElement();
			}

			// Token: 0x04000EF2 RID: 3826
			internal static readonly XmlDictionaryString ElementName = XD.XmlEncryptionDictionary.DataReference;

			// Token: 0x04000EF3 RID: 3827
			internal static readonly XmlDictionaryString NamespaceUri = EncryptedType.NamespaceUri;
		}
	}
}
