using System;
using System.Reflection;

namespace System.Web.Helpers.Claims
{
	// Token: 0x02000026 RID: 38
	internal sealed class Claim
	{
		// Token: 0x06000112 RID: 274 RVA: 0x000047DC File Offset: 0x000029DC
		public Claim(string claimType, string value)
		{
			this.ClaimType = claimType;
			this.Value = value;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000113 RID: 275 RVA: 0x000047F2 File Offset: 0x000029F2
		// (set) Token: 0x06000114 RID: 276 RVA: 0x000047FA File Offset: 0x000029FA
		public string ClaimType { get; private set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00004803 File Offset: 0x00002A03
		// (set) Token: 0x06000116 RID: 278 RVA: 0x0000480B File Offset: 0x00002A0B
		public string Value { get; private set; }

		// Token: 0x06000117 RID: 279 RVA: 0x00004814 File Offset: 0x00002A14
		internal static Claim Create<TClaim>(TClaim claim)
		{
			return Claim.ClaimFactory<TClaim>.Create(claim);
		}

		// Token: 0x02000027 RID: 39
		private static class ClaimFactory<TClaim>
		{
			// Token: 0x06000118 RID: 280 RVA: 0x0000481C File Offset: 0x00002A1C
			public static Claim Create(TClaim claim)
			{
				return new Claim(Claim.ClaimFactory<TClaim>._claimTypeGetter(claim), Claim.ClaimFactory<TClaim>._valueGetter(claim));
			}

			// Token: 0x06000119 RID: 281 RVA: 0x00004839 File Offset: 0x00002A39
			private static Func<TClaim, string> CreateClaimTypeGetter()
			{
				return Claim.ClaimFactory<TClaim>.CreateGeneralPropertyGetter("ClaimType") ?? Claim.ClaimFactory<TClaim>.CreateGeneralPropertyGetter("Type");
			}

			// Token: 0x0600011A RID: 282 RVA: 0x00004854 File Offset: 0x00002A54
			private static Func<TClaim, string> CreateGeneralPropertyGetter(string propertyName)
			{
				PropertyInfo property = typeof(TClaim).GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public, null, typeof(string), Type.EmptyTypes, null);
				if (property == null)
				{
					return null;
				}
				MethodInfo getMethod = property.GetGetMethod();
				return (Func<TClaim, string>)Delegate.CreateDelegate(typeof(Func<TClaim, string>), getMethod);
			}

			// Token: 0x0600011B RID: 283 RVA: 0x000048AC File Offset: 0x00002AAC
			private static Func<TClaim, string> CreateValueGetter()
			{
				return Claim.ClaimFactory<TClaim>.CreateGeneralPropertyGetter("Value");
			}

			// Token: 0x0400005B RID: 91
			private static readonly Func<TClaim, string> _claimTypeGetter = Claim.ClaimFactory<TClaim>.CreateClaimTypeGetter();

			// Token: 0x0400005C RID: 92
			private static readonly Func<TClaim, string> _valueGetter = Claim.ClaimFactory<TClaim>.CreateValueGetter();
		}
	}
}
