using System;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Web;
using System.Web.Script.Serialization;
using Telerik.Web.UI.CloudUpload;
using Telerik.Web.UI.Common;

namespace Telerik.Web.UI
{
	// Token: 0x020001AF RID: 431
	public class CloudUploadHandler : PreventableHandler, IHttpHandler, IDisposable
	{
		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x00039F24 File Offset: 0x00038124
		// (set) Token: 0x06000F8E RID: 3982 RVA: 0x00039F2C File Offset: 0x0003812C
		internal HttpContext Context { get; set; }

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06000F8F RID: 3983 RVA: 0x00039F35 File Offset: 0x00038135
		internal JavaScriptSerializer Serializer
		{
			get
			{
				if (this._serializer == null)
				{
					this._serializer = new JavaScriptSerializer();
				}
				return this._serializer;
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06000F90 RID: 3984 RVA: 0x00039F50 File Offset: 0x00038150
		internal EventHandlerList Events
		{
			get
			{
				if (this._events == null)
				{
					this._events = new EventHandlerList();
				}
				return this._events;
			}
		}

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06000F91 RID: 3985 RVA: 0x00039F6B File Offset: 0x0003816B
		// (remove) Token: 0x06000F92 RID: 3986 RVA: 0x00039F7E File Offset: 0x0003817E
		protected internal event CustomProviderSetupEventHandler CustomProviderSetup
		{
			add
			{
				this.Events.AddHandler(CloudUploadHandler.CustomProviderSetupEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(CloudUploadHandler.CustomProviderSetupEvent, value);
			}
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x00039F99 File Offset: 0x00038199
		private ICloudUploadConfiguration EnsureConfiguration()
		{
			return this.exceptionThrower.ThrowIfFails<ICloudUploadConfiguration>(() => this.EnsureValidConfiguration());
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x00039FB4 File Offset: 0x000381B4
		private ICloudUploadConfiguration EnsureValidConfiguration()
		{
			string input = this.Context.Request["rcuConfigurationData"];
			if (!this.IsValidHMac(input))
			{
				throw new CryptographicException("The hash is not valid!");
			}
			string input2 = CryptoService.GetService("").DecryptWithMachineKey(this.GetEncryptedText(input));
			return this.Serializer.Deserialize<CloudUploadConfiguration>(input2);
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x0003A010 File Offset: 0x00038210
		internal bool IsValidHMac(string input)
		{
			return HmacService.GetService().HMAC256(this.GetEncryptedText(input)) == this.GetHash(input);
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x0003A02F File Offset: 0x0003822F
		internal string GetHash(string input)
		{
			return input.Substring(input.Length - HmacService.GetService().GetHmacLength());
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x0003A048 File Offset: 0x00038248
		internal string GetEncryptedText(string input)
		{
			return input.Substring(0, input.Length - HmacService.GetService().GetHmacLength());
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x0003A064 File Offset: 0x00038264
		private void EnsureWorker()
		{
			ICloudUploadConfiguration cloudUploadConfiguration = this.EnsureConfiguration();
			CustomProviderSetupEventArgs customProviderSetupEventArgs = new CustomProviderSetupEventArgs();
			CustomProviderSetupEventHandler customProviderSetupEventHandler = (CustomProviderSetupEventHandler)this.Events[CloudUploadHandler.CustomProviderSetupEvent];
			customProviderSetupEventHandler(this, customProviderSetupEventArgs);
			ProviderType valueOrDefault = cloudUploadConfiguration.ProviderType.GetValueOrDefault();
			ProviderType? providerType;
			if (providerType != null)
			{
				switch (valueOrDefault)
				{
				case ProviderType.Amazon:
				{
					Type type = (customProviderSetupEventArgs.ProviderType != null) ? customProviderSetupEventArgs.ProviderType : typeof(AmazonS3Provider);
					string name = (!string.IsNullOrEmpty(customProviderSetupEventArgs.Name)) ? customProviderSetupEventArgs.Name : "Amazon";
					this.worker = new AmazonWorker(this.Context, cloudUploadConfiguration, name, type);
					break;
				}
				case ProviderType.Everlive:
				{
					Type type = (customProviderSetupEventArgs.ProviderType != null) ? customProviderSetupEventArgs.ProviderType : typeof(EverliveProvider);
					string name = (!string.IsNullOrEmpty(customProviderSetupEventArgs.Name)) ? customProviderSetupEventArgs.Name : "Everlive";
					this.worker = new EverliveWorker(this.Context, cloudUploadConfiguration, name, type);
					break;
				}
				case ProviderType.Azure:
				{
					Type type = (customProviderSetupEventArgs.ProviderType != null) ? customProviderSetupEventArgs.ProviderType : typeof(AzureProvider);
					string name = (!string.IsNullOrEmpty(customProviderSetupEventArgs.Name)) ? customProviderSetupEventArgs.Name : "Azure";
					this.worker = new AzureWorker(this.Context, cloudUploadConfiguration, name, type);
					break;
				}
				}
			}
			this.worker.GenericProvider.EnsureWebClient();
			this.worker.EnsureData();
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x0003A1E4 File Offset: 0x000383E4
		private void AttachWorkerHandlers()
		{
			this.worker.KeyNameSetup += this.SetKeyName;
			this.worker.MetaDataSetup += this.SetMetaData;
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x0003A218 File Offset: 0x00038418
		private void DetachWorkerHandlers()
		{
			this.worker.KeyNameSetup -= this.SetKeyName;
			this.worker.MetaDataSetup -= this.SetMetaData;
			this.CustomProviderSetup -= this.SetCustomProvider;
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x0003A268 File Offset: 0x00038468
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.worker.Dispose();
				this._events.Dispose();
			}
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x0003A284 File Offset: 0x00038484
		public void ProcessRequest(HttpContext context)
		{
			bool flag = base.CheckPreventHandler("Telerik.Web.DisableCloudUploadHandler", context, "rcu");
			if (flag)
			{
				base.CompleteRequest(context.ApplicationInstance, 404);
				return;
			}
			this.Context = context;
			this.CustomProviderSetup += this.SetCustomProvider;
			this.EnsureWorker();
			this.AttachWorkerHandlers();
			this.worker.Process();
			this.DetachWorkerHandlers();
			context.Response.Write(this.Serializer.Serialize(this.worker.ResponseMetaData));
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x0003A310 File Offset: 0x00038510
		public virtual void SetMetaData(object sender, SetMetaDataEventArgs e)
		{
			e.MetaData.Add("fileName", this.worker.GetFileName());
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x0003A32D File Offset: 0x0003852D
		public virtual void SetKeyName(object sender, SetKeyNameEventArgs e)
		{
			e.KeyName = string.Format("{0}{1}_{2}", e.SubFolderStructure, Guid.NewGuid(), e.OriginalFileName);
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x0003A355 File Offset: 0x00038555
		public virtual void SetCustomProvider(object sender, CustomProviderSetupEventArgs e)
		{
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06000FA0 RID: 4000 RVA: 0x0003A357 File Offset: 0x00038557
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x0003A35A File Offset: 0x0003855A
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x0003A369 File Offset: 0x00038569
		// Note: this type is marked as 'beforefieldinit'.
		static CloudUploadHandler()
		{
			CloudUploadHandler.CustomProviderSetupEvent = new object();
		}

		// Token: 0x0400046C RID: 1132
		private JavaScriptSerializer _serializer;

		// Token: 0x0400046D RID: 1133
		internal BaseWorker worker;

		// Token: 0x0400046E RID: 1134
		private EventHandlerList _events;

		// Token: 0x0400046F RID: 1135
		private readonly ICryptoExceptionThrower exceptionThrower = new CryptoExceptionThrower();
	}
}
