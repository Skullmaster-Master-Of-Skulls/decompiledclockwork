using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020006FB RID: 1787
	public sealed class HttpHandlersSection : ConfigurationSection
	{
		// Token: 0x06005647 RID: 22087 RVA: 0x0012E4E0 File Offset: 0x0012C6E0
		static HttpHandlersSection()
		{
			HttpHandlersSection._properties = new ConfigurationPropertyCollection();
			HttpHandlersSection._properties.Add(HttpHandlersSection._propHandlers);
		}

		// Token: 0x170018EA RID: 6378
		// (get) Token: 0x06005649 RID: 22089 RVA: 0x0012E512 File Offset: 0x0012C712
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HttpHandlersSection._properties;
			}
		}

		// Token: 0x170018EB RID: 6379
		// (get) Token: 0x0600564A RID: 22090 RVA: 0x0012E519 File Offset: 0x0012C719
		[ConfigurationProperty("", IsDefaultCollection = true)]
		public HttpHandlerActionCollection Handlers
		{
			get
			{
				return (HttpHandlerActionCollection)base[HttpHandlersSection._propHandlers];
			}
		}

		// Token: 0x0600564B RID: 22091 RVA: 0x0012E52C File Offset: 0x0012C72C
		internal bool ValidateHandlers()
		{
			if (!this._validated)
			{
				lock (this)
				{
					if (!this._validated)
					{
						foreach (object obj in this.Handlers)
						{
							HttpHandlerAction httpHandlerAction = (HttpHandlerAction)obj;
							httpHandlerAction.InitValidateInternal();
						}
						this._validated = true;
					}
				}
			}
			return this._validated;
		}

		// Token: 0x0600564C RID: 22092 RVA: 0x0012E5C8 File Offset: 0x0012C7C8
		internal HttpHandlerAction FindMapping(string verb, VirtualPath path)
		{
			this.ValidateHandlers();
			for (int i = 0; i < this.Handlers.Count; i++)
			{
				HttpHandlerAction httpHandlerAction = this.Handlers[i];
				if (httpHandlerAction.IsMatch(verb, path))
				{
					return httpHandlerAction;
				}
			}
			return null;
		}

		// Token: 0x04002DD3 RID: 11731
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002DD4 RID: 11732
		private static readonly ConfigurationProperty _propHandlers = new ConfigurationProperty(null, typeof(HttpHandlerActionCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x04002DD5 RID: 11733
		private bool _validated;
	}
}
