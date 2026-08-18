using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime;
using System.Runtime.DurableInstancing;
using System.Text;
using System.Xml.Linq;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008B5 RID: 2229
	public sealed class CorrelationKey : InstanceKey
	{
		// Token: 0x060054F1 RID: 21745 RVA: 0x00138588 File Offset: 0x00136788
		private CorrelationKey(string keyString, XNamespace provider) : base(CorrelationKey.GenerateKey(keyString), new Dictionary<XName, InstanceValue>(2)
		{
			{
				provider.GetName("KeyString"),
				new InstanceValue(keyString, InstanceValueOptions.Optional)
			},
			{
				WorkflowNamespace.KeyProvider,
				new InstanceValue(provider.NamespaceName, InstanceValueOptions.Optional)
			}
		})
		{
			this.KeyString = keyString;
		}

		// Token: 0x060054F2 RID: 21746 RVA: 0x001385DD File Offset: 0x001367DD
		private CorrelationKey(ReadOnlyDictionaryInternal<string, string> keyData, string scopeName, XNamespace provider) : this(CorrelationKey.GenerateKeyString(keyData, scopeName, provider.NamespaceName), provider)
		{
			this.KeyData = keyData;
			this.Provider = provider;
		}

		// Token: 0x060054F3 RID: 21747 RVA: 0x00138601 File Offset: 0x00136801
		public CorrelationKey(IDictionary<string, string> keyData, XName scopeName, XNamespace provider) : this((keyData == null) ? CorrelationKey.emptyDictionary : CorrelationKey.MakeReadonlyCopy(keyData), (scopeName != null) ? scopeName.ToString() : null, provider ?? CorrelationKey.CorrelationNamespace)
		{
			this.ScopeName = scopeName;
		}

		// Token: 0x060054F4 RID: 21748 RVA: 0x0013863C File Offset: 0x0013683C
		private static ReadOnlyDictionaryInternal<string, string> MakeReadonlyCopy(IDictionary<string, string> dictionary)
		{
			IDictionary<string, string> dictionary2;
			if (dictionary.IsReadOnly)
			{
				dictionary2 = dictionary;
			}
			else
			{
				dictionary2 = new Dictionary<string, string>(dictionary);
			}
			return new ReadOnlyDictionaryInternal<string, string>(dictionary2);
		}

		// Token: 0x170014E0 RID: 5344
		// (get) Token: 0x060054F5 RID: 21749 RVA: 0x00138662 File Offset: 0x00136862
		// (set) Token: 0x060054F6 RID: 21750 RVA: 0x0013866A File Offset: 0x0013686A
		public IDictionary<string, string> KeyData { get; private set; }

		// Token: 0x170014E1 RID: 5345
		// (get) Token: 0x060054F7 RID: 21751 RVA: 0x00138673 File Offset: 0x00136873
		// (set) Token: 0x060054F8 RID: 21752 RVA: 0x0013867B File Offset: 0x0013687B
		public XName ScopeName { get; private set; }

		// Token: 0x170014E2 RID: 5346
		// (get) Token: 0x060054F9 RID: 21753 RVA: 0x00138684 File Offset: 0x00136884
		// (set) Token: 0x060054FA RID: 21754 RVA: 0x0013868C File Offset: 0x0013688C
		public XNamespace Provider { get; private set; }

		// Token: 0x170014E3 RID: 5347
		// (get) Token: 0x060054FB RID: 21755 RVA: 0x00138695 File Offset: 0x00136895
		// (set) Token: 0x060054FC RID: 21756 RVA: 0x0013869D File Offset: 0x0013689D
		public string KeyString { get; private set; }

		// Token: 0x170014E4 RID: 5348
		// (get) Token: 0x060054FD RID: 21757 RVA: 0x001386A6 File Offset: 0x001368A6
		// (set) Token: 0x060054FE RID: 21758 RVA: 0x001386AE File Offset: 0x001368AE
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				if (!base.IsValid)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("CannotSetNameOnTheInvalidKey")));
				}
				this.name = value;
			}
		}

		// Token: 0x060054FF RID: 21759 RVA: 0x001386DC File Offset: 0x001368DC
		private static Guid GenerateKey(string keyString)
		{
			byte[] bytes = Encoding.Unicode.GetBytes(keyString);
			byte[] b = HashHelper.ComputeHash(bytes);
			return new Guid(b);
		}

		// Token: 0x06005500 RID: 21760 RVA: 0x00138704 File Offset: 0x00136904
		private static string GenerateKeyString(ReadOnlyDictionaryInternal<string, string> keyData, string scopeName, string provider)
		{
			if (string.IsNullOrEmpty(scopeName))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("scopeName", SR.GetString("ScopeNameMustBeSpecified"));
			}
			if (provider.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("provider", SR.GetString("ProviderCannotBeEmptyString"));
			}
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			SortedList<string, string> sortedList = new SortedList<string, string>(keyData, StringComparer.Ordinal);
			stringBuilder2.Append(sortedList.Count.ToString(NumberFormatInfo.InvariantInfo));
			stringBuilder2.Append('.');
			for (int i = 0; i < sortedList.Count; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append('&');
				}
				stringBuilder.Append(sortedList.Keys[i]);
				stringBuilder.Append('=');
				stringBuilder.Append(sortedList.Values[i]);
				stringBuilder2.Append(sortedList.Keys[i].Length.ToString(NumberFormatInfo.InvariantInfo));
				stringBuilder2.Append('.');
				stringBuilder2.Append(sortedList.Values[i].Length.ToString(NumberFormatInfo.InvariantInfo));
				stringBuilder2.Append('.');
			}
			if (sortedList.Count > 0)
			{
				stringBuilder.Append(',');
			}
			stringBuilder.Append(scopeName);
			stringBuilder.Append(',');
			stringBuilder.Append(provider);
			stringBuilder2.Append(scopeName.Length.ToString(NumberFormatInfo.InvariantInfo));
			stringBuilder2.Append('.');
			stringBuilder2.Append(provider.Length.ToString(NumberFormatInfo.InvariantInfo));
			stringBuilder.Append('|');
			stringBuilder.Append(stringBuilder2);
			return stringBuilder.ToString();
		}

		// Token: 0x0400334C RID: 13132
		private static readonly XNamespace CorrelationNamespace = XNamespace.Get("urn:microsoft-com:correlation");

		// Token: 0x0400334D RID: 13133
		private static readonly ReadOnlyDictionaryInternal<string, string> emptyDictionary = new ReadOnlyDictionaryInternal<string, string>(new Dictionary<string, string>(0));

		// Token: 0x0400334E RID: 13134
		private string name;
	}
}
