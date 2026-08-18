using System;
using Owin;

namespace Microsoft.Owin.Security.DataProtection
{
	// Token: 0x0200000A RID: 10
	public static class AppBuilderExtensions
	{
		// Token: 0x06000011 RID: 17 RVA: 0x000022F4 File Offset: 0x000004F4
		public static void SetDataProtectionProvider(this IAppBuilder app, IDataProtectionProvider dataProtectionProvider)
		{
			if (app == null)
			{
				throw new ArgumentNullException("app");
			}
			if (dataProtectionProvider == null)
			{
				app.Properties.Remove("security.DataProtectionProvider");
				return;
			}
			app.Properties["security.DataProtectionProvider"] = new Func<string[], Tuple<Func<byte[], byte[]>, Func<byte[], byte[]>>>(delegate(string[] purposes)
			{
				IDataProtector @object = dataProtectionProvider.Create(purposes);
				return new Tuple<Func<byte[], byte[]>, Func<byte[], byte[]>>(new Func<byte[], byte[]>(@object.Protect), new Func<byte[], byte[]>(@object.Unprotect));
			});
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000235C File Offset: 0x0000055C
		public static IDataProtectionProvider GetDataProtectionProvider(this IAppBuilder app)
		{
			if (app == null)
			{
				throw new ArgumentNullException("app");
			}
			object obj;
			if (app.Properties.TryGetValue("security.DataProtectionProvider", out obj))
			{
				Func<string[], Tuple<Func<byte[], byte[]>, Func<byte[], byte[]>>> func = obj as Func<string[], Tuple<Func<byte[], byte[]>, Func<byte[], byte[]>>>;
				if (func != null)
				{
					return new AppBuilderExtensions.CallDataProtectionProvider(func);
				}
			}
			return null;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000023A0 File Offset: 0x000005A0
		public static IDataProtector CreateDataProtector(this IAppBuilder app, params string[] purposes)
		{
			if (app == null)
			{
				throw new ArgumentNullException("app");
			}
			IDataProtectionProvider dataProtectionProvider = app.GetDataProtectionProvider();
			if (dataProtectionProvider == null)
			{
				dataProtectionProvider = AppBuilderExtensions.FallbackDataProtectionProvider(app);
			}
			return dataProtectionProvider.Create(purposes);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023D3 File Offset: 0x000005D3
		private static IDataProtectionProvider FallbackDataProtectionProvider(IAppBuilder app)
		{
			return new DpapiDataProtectionProvider(AppBuilderExtensions.GetAppName(app));
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000023E0 File Offset: 0x000005E0
		private static string GetAppName(IAppBuilder app)
		{
			object obj;
			if (app.Properties.TryGetValue("host.AppName", out obj))
			{
				string text = obj as string;
				if (!string.IsNullOrEmpty(text))
				{
					return text;
				}
			}
			throw new NotSupportedException(Resources.Exception_DefaultDpapiRequiresAppNameKey);
		}

		// Token: 0x0200000C RID: 12
		private class CallDataProtectionProvider : IDataProtectionProvider
		{
			// Token: 0x06000017 RID: 23 RVA: 0x0000241C File Offset: 0x0000061C
			public CallDataProtectionProvider(Func<string[], Tuple<Func<byte[], byte[]>, Func<byte[], byte[]>>> create)
			{
				this._create = create;
			}

			// Token: 0x06000018 RID: 24 RVA: 0x0000242C File Offset: 0x0000062C
			public IDataProtector Create(params string[] purposes)
			{
				Tuple<Func<byte[], byte[]>, Func<byte[], byte[]>> tuple = this._create(purposes);
				return new AppBuilderExtensions.CallDataProtectionProvider.CallDataProtection(tuple.Item1, tuple.Item2);
			}

			// Token: 0x0400000B RID: 11
			private readonly Func<string[], Tuple<Func<byte[], byte[]>, Func<byte[], byte[]>>> _create;

			// Token: 0x0200000E RID: 14
			private class CallDataProtection : IDataProtector
			{
				// Token: 0x0600001B RID: 27 RVA: 0x00002457 File Offset: 0x00000657
				public CallDataProtection(Func<byte[], byte[]> protect, Func<byte[], byte[]> unprotect)
				{
					this._protect = protect;
					this._unprotect = unprotect;
				}

				// Token: 0x0600001C RID: 28 RVA: 0x0000246D File Offset: 0x0000066D
				public byte[] Protect(byte[] userData)
				{
					return this._protect(userData);
				}

				// Token: 0x0600001D RID: 29 RVA: 0x0000247B File Offset: 0x0000067B
				public byte[] Unprotect(byte[] protectedData)
				{
					return this._unprotect(protectedData);
				}

				// Token: 0x0400000C RID: 12
				private readonly Func<byte[], byte[]> _protect;

				// Token: 0x0400000D RID: 13
				private readonly Func<byte[], byte[]> _unprotect;
			}
		}
	}
}
