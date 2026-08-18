using System;
using System.Collections.Generic;

namespace System.ServiceModel
{
	// Token: 0x02000107 RID: 263
	public class UriSchemeKeyedCollection : SynchronizedKeyedCollection<string, Uri>
	{
		// Token: 0x060005E3 RID: 1507 RVA: 0x0001AB23 File Offset: 0x00018D23
		internal UriSchemeKeyedCollection(object syncRoot) : base(syncRoot)
		{
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0001AB2C File Offset: 0x00018D2C
		public UriSchemeKeyedCollection(params Uri[] addresses)
		{
			if (addresses == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("addresses");
			}
			for (int i = 0; i < addresses.Length; i++)
			{
				base.Add(addresses[i]);
			}
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0001AB69 File Offset: 0x00018D69
		protected override string GetKeyForItem(Uri item)
		{
			return item.Scheme;
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0001AB74 File Offset: 0x00018D74
		protected override void InsertItem(int index, Uri item)
		{
			UriSchemeKeyedCollection.ValidateBaseAddress(item, "item");
			if (base.Contains(item.Scheme))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("item", SR.GetString("BaseAddressDuplicateScheme", new object[]
				{
					item.Scheme
				}));
			}
			base.InsertItem(index, item);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0001ABCC File Offset: 0x00018DCC
		protected override void SetItem(int index, Uri item)
		{
			UriSchemeKeyedCollection.ValidateBaseAddress(item, "item");
			if (base[index].Scheme != item.Scheme && base.Contains(item.Scheme))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("item", SR.GetString("BaseAddressDuplicateScheme", new object[]
				{
					item.Scheme
				}));
			}
			base.SetItem(index, item);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0001AC3C File Offset: 0x00018E3C
		internal static void ValidateBaseAddress(Uri uri, string argumentName)
		{
			if (uri == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull(argumentName);
			}
			if (!uri.IsAbsoluteUri)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(argumentName, SR.GetString("BaseAddressMustBeAbsolute"));
			}
			if (!string.IsNullOrEmpty(uri.UserInfo))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(argumentName, SR.GetString("BaseAddressCannotHaveUserInfo"));
			}
			if (!string.IsNullOrEmpty(uri.Query))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(argumentName, SR.GetString("BaseAddressCannotHaveQuery"));
			}
			if (!string.IsNullOrEmpty(uri.Fragment))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(argumentName, SR.GetString("BaseAddressCannotHaveFragment"));
			}
		}
	}
}
