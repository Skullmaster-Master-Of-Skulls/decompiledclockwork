using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Principal;
using System.Web.Security;

namespace System.Web.Helpers.Claims
{
	// Token: 0x0200002A RID: 42
	internal sealed class ClaimsIdentityConverter
	{
		// Token: 0x06000124 RID: 292 RVA: 0x0000497F File Offset: 0x00002B7F
		internal ClaimsIdentityConverter(Func<IIdentity, ClaimsIdentity>[] converters)
		{
			this._converters = converters;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000125 RID: 293 RVA: 0x0000498E File Offset: 0x00002B8E
		public static ClaimsIdentityConverter Default
		{
			get
			{
				return ClaimsIdentityConverter._default;
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00004995 File Offset: 0x00002B95
		private static bool IsGrandfatheredIdentityType(IIdentity claimsIdentity)
		{
			return claimsIdentity is FormsIdentity || claimsIdentity is WindowsIdentity || claimsIdentity is GenericIdentity;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000049B4 File Offset: 0x00002BB4
		public ClaimsIdentity TryConvert(IIdentity identity)
		{
			if (ClaimsIdentityConverter.IsGrandfatheredIdentityType(identity))
			{
				return null;
			}
			for (int i = 0; i < this._converters.Length; i++)
			{
				ClaimsIdentity claimsIdentity = this._converters[i](identity);
				if (claimsIdentity != null)
				{
					return claimsIdentity;
				}
			}
			return null;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000049F4 File Offset: 0x00002BF4
		private static void AddToList(IList<Func<IIdentity, ClaimsIdentity>> converters, Type claimsIdentityType, Type claimType)
		{
			if (claimsIdentityType != null && claimType != null)
			{
				MethodInfo method = ClaimsIdentityConverter._claimsIdentityTryConvertOpenMethod.MakeGenericMethod(new Type[]
				{
					claimsIdentityType,
					claimType
				});
				Func<IIdentity, ClaimsIdentity> item = (Func<IIdentity, ClaimsIdentity>)Delegate.CreateDelegate(typeof(Func<IIdentity, ClaimsIdentity>), method);
				converters.Add(item);
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00004A4C File Offset: 0x00002C4C
		private static Func<IIdentity, ClaimsIdentity>[] GetDefaultConverters()
		{
			List<Func<IIdentity, ClaimsIdentity>> list = new List<Func<IIdentity, ClaimsIdentity>>();
			if (AppDomain.CurrentDomain.IsHomogenous && AppDomain.CurrentDomain.IsFullyTrusted)
			{
				Type type = Type.GetType("Microsoft.IdentityModel.Claims.IClaimsIdentity, Microsoft.IdentityModel, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");
				Type type2 = Type.GetType("Microsoft.IdentityModel.Claims.Claim, Microsoft.IdentityModel, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");
				ClaimsIdentityConverter.AddToList(list, type, type2);
			}
			Module module = typeof(object).Module;
			Type type3 = module.GetType("System.Security.Claims.ClaimsIdentity");
			Type type4 = module.GetType("System.Security.Claims.Claim");
			ClaimsIdentityConverter.AddToList(list, type3, type4);
			return list.ToArray();
		}

		// Token: 0x0400005F RID: 95
		private static readonly MethodInfo _claimsIdentityTryConvertOpenMethod = typeof(ClaimsIdentity).GetMethod("TryConvert", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

		// Token: 0x04000060 RID: 96
		private static readonly ClaimsIdentityConverter _default = new ClaimsIdentityConverter(ClaimsIdentityConverter.GetDefaultConverters());

		// Token: 0x04000061 RID: 97
		private readonly Func<IIdentity, ClaimsIdentity>[] _converters;
	}
}
