using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008D9 RID: 2265
	internal abstract class MsmqBindingFilter
	{
		// Token: 0x06005630 RID: 22064 RVA: 0x0013B8BC File Offset: 0x00139ABC
		public MsmqBindingFilter(string path, MsmqUri.IAddressTranslator addressing)
		{
			this.prefix = path;
			this.addressing = addressing;
			if (this.prefix.Length > 0 && this.prefix[0] == '/')
			{
				this.prefix = this.prefix.Substring(1);
			}
			if (this.prefix.Length > 0 && this.prefix[this.prefix.Length - 1] != '/')
			{
				this.prefix += "/";
			}
		}

		// Token: 0x1700150C RID: 5388
		// (get) Token: 0x06005631 RID: 22065 RVA: 0x0013B94D File Offset: 0x00139B4D
		public string CanonicalPrefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x06005632 RID: 22066 RVA: 0x0013B955 File Offset: 0x00139B55
		public int Match(string name)
		{
			if (string.Compare(this.CanonicalPrefix, 0, name, 0, this.CanonicalPrefix.Length, StringComparison.OrdinalIgnoreCase) == 0)
			{
				return this.CanonicalPrefix.Length;
			}
			return -1;
		}

		// Token: 0x06005633 RID: 22067 RVA: 0x0013B980 File Offset: 0x00139B80
		public Uri CreateServiceUri(string host, string name, bool isPrivate)
		{
			return this.addressing.CreateUri(host, name, isPrivate);
		}

		// Token: 0x06005634 RID: 22068
		public abstract object MatchFound(string host, string name, bool isPrivate);

		// Token: 0x06005635 RID: 22069
		public abstract void MatchLost(string host, string name, bool isPrivate, object callbackState);

		// Token: 0x0400354C RID: 13644
		private string prefix;

		// Token: 0x0400354D RID: 13645
		private MsmqUri.IAddressTranslator addressing;
	}
}
