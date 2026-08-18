using System;
using System.Configuration;
using System.Security.Permissions;

namespace System.Diagnostics
{
	// Token: 0x0200049C RID: 1180
	[ConfigurationCollection(typeof(ListenerElement))]
	internal class ListenerElementsCollection : ConfigurationElementCollection
	{
		// Token: 0x17000A95 RID: 2709
		public ListenerElement this[string name]
		{
			get
			{
				return (ListenerElement)base.BaseGet(name);
			}
		}

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06002BCD RID: 11213 RVA: 0x000C63DC File Offset: 0x000C45DC
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.AddRemoveClearMap;
			}
		}

		// Token: 0x06002BCE RID: 11214 RVA: 0x000C63DF File Offset: 0x000C45DF
		protected override ConfigurationElement CreateNewElement()
		{
			return new ListenerElement(true);
		}

		// Token: 0x06002BCF RID: 11215 RVA: 0x000C63E7 File Offset: 0x000C45E7
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ListenerElement)element).Name;
		}

		// Token: 0x06002BD0 RID: 11216 RVA: 0x000C63F4 File Offset: 0x000C45F4
		public TraceListenerCollection GetRuntimeObject()
		{
			TraceListenerCollection traceListenerCollection = new TraceListenerCollection();
			bool flag = false;
			foreach (object obj in this)
			{
				ListenerElement listenerElement = (ListenerElement)obj;
				if (!flag && !listenerElement._isAddedByDefault)
				{
					new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
					flag = true;
				}
				traceListenerCollection.Add(listenerElement.GetRuntimeObject());
			}
			return traceListenerCollection;
		}

		// Token: 0x06002BD1 RID: 11217 RVA: 0x000C6474 File Offset: 0x000C4674
		protected override void InitializeDefault()
		{
			this.InitializeDefaultInternal();
		}

		// Token: 0x06002BD2 RID: 11218 RVA: 0x000C647C File Offset: 0x000C467C
		internal void InitializeDefaultInternal()
		{
			this.BaseAdd(new ListenerElement(false)
			{
				Name = "Default",
				TypeName = typeof(DefaultTraceListener).FullName,
				_isAddedByDefault = true
			});
		}

		// Token: 0x06002BD3 RID: 11219 RVA: 0x000C64C0 File Offset: 0x000C46C0
		protected override void BaseAdd(ConfigurationElement element)
		{
			ListenerElement listenerElement = element as ListenerElement;
			if (listenerElement.Name.Equals("Default") && listenerElement.TypeName.Equals(typeof(DefaultTraceListener).FullName))
			{
				base.BaseAdd(listenerElement, false);
				return;
			}
			base.BaseAdd(listenerElement, this.ThrowOnDuplicate);
		}
	}
}
