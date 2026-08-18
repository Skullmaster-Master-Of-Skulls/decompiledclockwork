using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.AsyncUpload
{
	// Token: 0x02000074 RID: 116
	internal class AsyncUploadConfigurationConverter : JavaScriptConverter
	{
		// Token: 0x060004B0 RID: 1200 RVA: 0x0000BD9C File Offset: 0x00009F9C
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			IDictionary<string, object> dictionary2 = dictionary["TimeToLive"] as IDictionary<string, object>;
			IAsyncUploadConfiguration asyncUploadConfiguration = Activator.CreateInstance(type) as IAsyncUploadConfiguration;
			long value = Convert.ToInt64(dictionary2["Ticks"]);
			asyncUploadConfiguration.TimeToLive = TimeSpan.FromTicks(value);
			MethodInfo methodInfo = typeof(JavaScriptSerializer).GetMethod("ConvertToType", new Type[]
			{
				typeof(object)
			}, null).MakeGenericMethod(new Type[]
			{
				type
			});
			object obj = methodInfo.Invoke(new JavaScriptSerializer(), new object[]
			{
				dictionary
			});
			return this.MergeDefaultConfiguration(asyncUploadConfiguration, (IAsyncUploadConfiguration)obj);
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0000BE4F File Offset: 0x0000A04F
		private object MergeDefaultConfiguration(IAsyncUploadConfiguration config, IAsyncUploadConfiguration customObject)
		{
			customObject.TimeToLive = config.TimeToLive;
			return customObject;
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0000BE5E File Offset: 0x0000A05E
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x0000BE68 File Offset: 0x0000A068
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(AsyncUploadConfiguration)
				};
			}
		}
	}
}
