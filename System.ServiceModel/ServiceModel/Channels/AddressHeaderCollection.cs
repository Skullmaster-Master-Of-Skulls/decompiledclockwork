using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009B1 RID: 2481
	[__DynamicallyInvokable]
	public sealed class AddressHeaderCollection : ReadOnlyCollection<AddressHeader>
	{
		// Token: 0x06006157 RID: 24919 RVA: 0x0016AE52 File Offset: 0x00169052
		[__DynamicallyInvokable]
		public AddressHeaderCollection() : base(new List<AddressHeader>())
		{
		}

		// Token: 0x06006158 RID: 24920 RVA: 0x0016AE60 File Offset: 0x00169060
		[__DynamicallyInvokable]
		public AddressHeaderCollection(IEnumerable<AddressHeader> addressHeaders) : base(new List<AddressHeader>(addressHeaders))
		{
			IList<AddressHeader> list = addressHeaders as IList<AddressHeader>;
			if (list != null)
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i] == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("MessageHeaderIsNull0")));
					}
				}
				return;
			}
			if (!LocalAppContextSwitches.DisableAddressHeaderCollectionValidation)
			{
				using (IEnumerator<AddressHeader> enumerator = addressHeaders.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current == null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("MessageHeaderIsNull0")));
						}
					}
				}
			}
		}

		// Token: 0x17001778 RID: 6008
		// (get) Token: 0x06006159 RID: 24921 RVA: 0x0016AF10 File Offset: 0x00169110
		internal static AddressHeaderCollection EmptyHeaderCollection
		{
			get
			{
				return AddressHeaderCollection.emptyHeaderCollection;
			}
		}

		// Token: 0x17001779 RID: 6009
		// (get) Token: 0x0600615A RID: 24922 RVA: 0x0016AF17 File Offset: 0x00169117
		private int InternalCount
		{
			get
			{
				if (this == AddressHeaderCollection.emptyHeaderCollection)
				{
					return 0;
				}
				return base.Count;
			}
		}

		// Token: 0x0600615B RID: 24923 RVA: 0x0016AF2C File Offset: 0x0016912C
		[__DynamicallyInvokable]
		public void AddHeadersTo(Message message)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			for (int i = 0; i < this.InternalCount; i++)
			{
				message.Headers.Add(base[i].ToMessageHeader());
			}
		}

		// Token: 0x0600615C RID: 24924 RVA: 0x0016AF74 File Offset: 0x00169174
		[__DynamicallyInvokable]
		public AddressHeader[] FindAll(string name, string ns)
		{
			if (name == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("name"));
			}
			if (ns == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("ns"));
			}
			List<AddressHeader> list = new List<AddressHeader>();
			for (int i = 0; i < base.Count; i++)
			{
				AddressHeader addressHeader = base[i];
				if (addressHeader.Name == name && addressHeader.Namespace == ns)
				{
					list.Add(addressHeader);
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600615D RID: 24925 RVA: 0x0016AFFC File Offset: 0x001691FC
		[__DynamicallyInvokable]
		public AddressHeader FindHeader(string name, string ns)
		{
			if (name == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("name"));
			}
			if (ns == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("ns"));
			}
			AddressHeader addressHeader = null;
			for (int i = 0; i < base.Count; i++)
			{
				AddressHeader addressHeader2 = base[i];
				if (addressHeader2.Name == name && addressHeader2.Namespace == ns)
				{
					if (addressHeader != null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("MultipleMessageHeaders", new object[]
						{
							name,
							ns
						})));
					}
					addressHeader = addressHeader2;
				}
			}
			return addressHeader;
		}

		// Token: 0x0600615E RID: 24926 RVA: 0x0016B0A0 File Offset: 0x001692A0
		internal bool IsEquivalent(AddressHeaderCollection col)
		{
			if (this.InternalCount != col.InternalCount)
			{
				return false;
			}
			StringBuilder builder = new StringBuilder();
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			this.PopulateHeaderDictionary(builder, dictionary);
			Dictionary<string, int> dictionary2 = new Dictionary<string, int>();
			col.PopulateHeaderDictionary(builder, dictionary2);
			if (dictionary.Count != dictionary2.Count)
			{
				return false;
			}
			foreach (KeyValuePair<string, int> keyValuePair in dictionary)
			{
				int num;
				if (!dictionary2.TryGetValue(keyValuePair.Key, out num))
				{
					return false;
				}
				if (num != keyValuePair.Value)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600615F RID: 24927 RVA: 0x0016B154 File Offset: 0x00169354
		internal void PopulateHeaderDictionary(StringBuilder builder, Dictionary<string, int> headers)
		{
			for (int i = 0; i < this.InternalCount; i++)
			{
				builder.Remove(0, builder.Length);
				string comparableForm = base[i].GetComparableForm(builder);
				if (headers.ContainsKey(comparableForm))
				{
					headers[comparableForm]++;
				}
				else
				{
					headers.Add(comparableForm, 1);
				}
			}
		}

		// Token: 0x06006160 RID: 24928 RVA: 0x0016B1B1 File Offset: 0x001693B1
		internal static AddressHeaderCollection ReadServiceParameters(XmlDictionaryReader reader)
		{
			return AddressHeaderCollection.ReadServiceParameters(reader, false);
		}

		// Token: 0x06006161 RID: 24929 RVA: 0x0016B1BC File Offset: 0x001693BC
		internal static AddressHeaderCollection ReadServiceParameters(XmlDictionaryReader reader, bool isReferenceProperty)
		{
			reader.MoveToContent();
			if (reader.IsEmptyElement)
			{
				reader.Skip();
				return null;
			}
			reader.ReadStartElement();
			List<AddressHeader> list = new List<AddressHeader>();
			while (reader.IsStartElement())
			{
				list.Add(new BufferedAddressHeader(reader, isReferenceProperty));
			}
			reader.ReadEndElement();
			return new AddressHeaderCollection(list);
		}

		// Token: 0x1700177A RID: 6010
		// (get) Token: 0x06006162 RID: 24930 RVA: 0x0016B210 File Offset: 0x00169410
		internal bool HasReferenceProperties
		{
			get
			{
				for (int i = 0; i < this.InternalCount; i++)
				{
					if (base[i].IsReferenceProperty)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x1700177B RID: 6011
		// (get) Token: 0x06006163 RID: 24931 RVA: 0x0016B240 File Offset: 0x00169440
		internal bool HasNonReferenceProperties
		{
			get
			{
				for (int i = 0; i < this.InternalCount; i++)
				{
					if (!base[i].IsReferenceProperty)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06006164 RID: 24932 RVA: 0x0016B270 File Offset: 0x00169470
		internal void WriteReferencePropertyContentsTo(XmlDictionaryWriter writer)
		{
			for (int i = 0; i < this.InternalCount; i++)
			{
				if (base[i].IsReferenceProperty)
				{
					base[i].WriteAddressHeader(writer);
				}
			}
		}

		// Token: 0x06006165 RID: 24933 RVA: 0x0016B2AC File Offset: 0x001694AC
		internal void WriteNonReferencePropertyContentsTo(XmlDictionaryWriter writer)
		{
			for (int i = 0; i < this.InternalCount; i++)
			{
				if (!base[i].IsReferenceProperty)
				{
					base[i].WriteAddressHeader(writer);
				}
			}
		}

		// Token: 0x06006166 RID: 24934 RVA: 0x0016B2E8 File Offset: 0x001694E8
		internal void WriteContentsTo(XmlDictionaryWriter writer)
		{
			for (int i = 0; i < this.InternalCount; i++)
			{
				base[i].WriteAddressHeader(writer);
			}
		}

		// Token: 0x040038D3 RID: 14547
		private static AddressHeaderCollection emptyHeaderCollection = new AddressHeaderCollection();
	}
}
