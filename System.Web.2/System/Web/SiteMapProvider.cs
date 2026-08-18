using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Web.UI;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x020000F8 RID: 248
	public abstract class SiteMapProvider : ProviderBase
	{
		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06000EFF RID: 3839 RVA: 0x0002A674 File Offset: 0x00028874
		public virtual SiteMapNode CurrentNode
		{
			get
			{
				HttpContext context = HttpContext.Current;
				SiteMapNode siteMapNode = this.ResolveSiteMapNode(context);
				if (siteMapNode == null)
				{
					siteMapNode = this.FindSiteMapNode(context);
				}
				return this.ReturnNodeIfAccessible(siteMapNode);
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06000F00 RID: 3840 RVA: 0x0002A6A3 File Offset: 0x000288A3
		// (set) Token: 0x06000F01 RID: 3841 RVA: 0x0002A6AB File Offset: 0x000288AB
		public bool EnableLocalization
		{
			get
			{
				return this._enableLocalization;
			}
			set
			{
				this._enableLocalization = value;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06000F02 RID: 3842 RVA: 0x0002A6B4 File Offset: 0x000288B4
		// (set) Token: 0x06000F03 RID: 3843 RVA: 0x0002A6BC File Offset: 0x000288BC
		public virtual SiteMapProvider ParentProvider
		{
			get
			{
				return this._parentProvider;
			}
			set
			{
				this._parentProvider = value;
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x0002A6C5 File Offset: 0x000288C5
		// (set) Token: 0x06000F05 RID: 3845 RVA: 0x0002A6CD File Offset: 0x000288CD
		public string ResourceKey
		{
			get
			{
				return this._resourceKey;
			}
			set
			{
				this._resourceKey = value;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x0002A6D8 File Offset: 0x000288D8
		public virtual SiteMapProvider RootProvider
		{
			get
			{
				if (this._rootProvider == null)
				{
					object @lock = this._lock;
					lock (@lock)
					{
						if (this._rootProvider == null)
						{
							Hashtable hashtable = new Hashtable();
							SiteMapProvider siteMapProvider = this;
							hashtable.Add(siteMapProvider, null);
							while (siteMapProvider.ParentProvider != null)
							{
								if (hashtable.Contains(siteMapProvider.ParentProvider))
								{
									throw new ProviderException(SR.GetString("SiteMapProvider_Circular_Provider"));
								}
								siteMapProvider = siteMapProvider.ParentProvider;
								hashtable.Add(siteMapProvider, null);
							}
							this._rootProvider = siteMapProvider;
						}
					}
				}
				return this._rootProvider;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x0002A778 File Offset: 0x00028978
		public virtual SiteMapNode RootNode
		{
			get
			{
				SiteMapNode rootNodeCore = this.GetRootNodeCore();
				return this.ReturnNodeIfAccessible(rootNodeCore);
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x0002A793 File Offset: 0x00028993
		public bool SecurityTrimmingEnabled
		{
			get
			{
				return this._securityTrimmingEnabled;
			}
		}

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06000F09 RID: 3849 RVA: 0x0002A79C File Offset: 0x0002899C
		// (remove) Token: 0x06000F0A RID: 3850 RVA: 0x0002A7D4 File Offset: 0x000289D4
		public event SiteMapResolveEventHandler SiteMapResolve;

		// Token: 0x06000F0B RID: 3851 RVA: 0x0002A809 File Offset: 0x00028A09
		protected virtual void AddNode(SiteMapNode node)
		{
			this.AddNode(node, null);
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x00003ABB File Offset: 0x00001CBB
		protected internal virtual void AddNode(SiteMapNode node, SiteMapNode parentNode)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x0002A814 File Offset: 0x00028A14
		public virtual SiteMapNode FindSiteMapNode(HttpContext context)
		{
			if (context == null)
			{
				return null;
			}
			string rawUrl = context.Request.RawUrl;
			SiteMapNode siteMapNode = this.FindSiteMapNode(rawUrl);
			if (siteMapNode == null)
			{
				int num = rawUrl.IndexOf("?", StringComparison.Ordinal);
				if (num != -1)
				{
					siteMapNode = this.FindSiteMapNode(rawUrl.Substring(0, num));
				}
				if (siteMapNode == null)
				{
					Page page = context.CurrentHandler as Page;
					if (page != null)
					{
						string clientQueryString = page.ClientQueryString;
						if (clientQueryString.Length > 0)
						{
							siteMapNode = this.FindSiteMapNode(context.Request.Path + "?" + clientQueryString);
						}
					}
					if (siteMapNode == null)
					{
						siteMapNode = this.FindSiteMapNode(context.Request.Path);
					}
				}
			}
			return siteMapNode;
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x0002A8B7 File Offset: 0x00028AB7
		public virtual SiteMapNode FindSiteMapNodeFromKey(string key)
		{
			return this.FindSiteMapNode(key);
		}

		// Token: 0x06000F0F RID: 3855
		public abstract SiteMapNode FindSiteMapNode(string rawUrl);

		// Token: 0x06000F10 RID: 3856
		public abstract SiteMapNodeCollection GetChildNodes(SiteMapNode node);

		// Token: 0x06000F11 RID: 3857 RVA: 0x0002A8C0 File Offset: 0x00028AC0
		public virtual SiteMapNode GetCurrentNodeAndHintAncestorNodes(int upLevel)
		{
			if (upLevel < -1)
			{
				throw new ArgumentOutOfRangeException("upLevel");
			}
			return this.CurrentNode;
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x0002A8D7 File Offset: 0x00028AD7
		public virtual SiteMapNode GetCurrentNodeAndHintNeighborhoodNodes(int upLevel, int downLevel)
		{
			if (upLevel < -1)
			{
				throw new ArgumentOutOfRangeException("upLevel");
			}
			if (downLevel < -1)
			{
				throw new ArgumentOutOfRangeException("downLevel");
			}
			return this.CurrentNode;
		}

		// Token: 0x06000F13 RID: 3859
		public abstract SiteMapNode GetParentNode(SiteMapNode node);

		// Token: 0x06000F14 RID: 3860 RVA: 0x0002A900 File Offset: 0x00028B00
		public virtual SiteMapNode GetParentNodeRelativeToCurrentNodeAndHintDownFromParent(int walkupLevels, int relativeDepthFromWalkup)
		{
			if (walkupLevels < 0)
			{
				throw new ArgumentOutOfRangeException("walkupLevels");
			}
			if (relativeDepthFromWalkup < 0)
			{
				throw new ArgumentOutOfRangeException("relativeDepthFromWalkup");
			}
			SiteMapNode currentNodeAndHintAncestorNodes = this.GetCurrentNodeAndHintAncestorNodes(walkupLevels);
			if (currentNodeAndHintAncestorNodes == null)
			{
				return null;
			}
			SiteMapNode parentNodesInternal = this.GetParentNodesInternal(currentNodeAndHintAncestorNodes, walkupLevels);
			if (parentNodesInternal == null)
			{
				return null;
			}
			this.HintNeighborhoodNodes(parentNodesInternal, 0, relativeDepthFromWalkup);
			return parentNodesInternal;
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x0002A950 File Offset: 0x00028B50
		public virtual SiteMapNode GetParentNodeRelativeToNodeAndHintDownFromParent(SiteMapNode node, int walkupLevels, int relativeDepthFromWalkup)
		{
			if (walkupLevels < 0)
			{
				throw new ArgumentOutOfRangeException("walkupLevels");
			}
			if (relativeDepthFromWalkup < 0)
			{
				throw new ArgumentOutOfRangeException("relativeDepthFromWalkup");
			}
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			this.HintAncestorNodes(node, walkupLevels);
			SiteMapNode parentNodesInternal = this.GetParentNodesInternal(node, walkupLevels);
			if (parentNodesInternal == null)
			{
				return null;
			}
			this.HintNeighborhoodNodes(parentNodesInternal, 0, relativeDepthFromWalkup);
			return parentNodesInternal;
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x0002A9A9 File Offset: 0x00028BA9
		private SiteMapNode GetParentNodesInternal(SiteMapNode node, int walkupLevels)
		{
			if (walkupLevels <= 0)
			{
				return node;
			}
			do
			{
				node = node.ParentNode;
				walkupLevels--;
			}
			while (node != null && walkupLevels != 0);
			return node;
		}

		// Token: 0x06000F17 RID: 3863
		protected internal abstract SiteMapNode GetRootNodeCore();

		// Token: 0x06000F18 RID: 3864 RVA: 0x0002A9C5 File Offset: 0x00028BC5
		protected static SiteMapNode GetRootNodeCoreFromProvider(SiteMapProvider provider)
		{
			return provider.GetRootNodeCore();
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x0002A9CD File Offset: 0x00028BCD
		public virtual void HintAncestorNodes(SiteMapNode node, int upLevel)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (upLevel < -1)
			{
				throw new ArgumentOutOfRangeException("upLevel");
			}
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x0002A9EC File Offset: 0x00028BEC
		public virtual void HintNeighborhoodNodes(SiteMapNode node, int upLevel, int downLevel)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (upLevel < -1)
			{
				throw new ArgumentOutOfRangeException("upLevel");
			}
			if (downLevel < -1)
			{
				throw new ArgumentOutOfRangeException("downLevel");
			}
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x0002AA1C File Offset: 0x00028C1C
		public override void Initialize(string name, NameValueCollection attributes)
		{
			if (attributes != null)
			{
				if (string.IsNullOrEmpty(attributes["description"]))
				{
					attributes.Remove("description");
					attributes.Add("description", base.GetType().Name);
				}
				ProviderUtil.GetAndRemoveBooleanAttribute(attributes, "securityTrimmingEnabled", this.Name, ref this._securityTrimmingEnabled);
			}
			base.Initialize(name, attributes);
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x0002AA80 File Offset: 0x00028C80
		public virtual bool IsAccessibleToUser(HttpContext context, SiteMapNode node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (!this.SecurityTrimmingEnabled)
			{
				return true;
			}
			if (node.Roles != null)
			{
				foreach (object obj in node.Roles)
				{
					string text = (string)obj;
					if (text == "*" || (context.User != null && context.User.IsInRole(text)))
					{
						return true;
					}
				}
			}
			VirtualPath virtualPath = node.VirtualPath;
			return !(virtualPath == null) && virtualPath.IsWithinAppRoot && Util.IsUserAllowedToPath(context, virtualPath);
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x00003ABB File Offset: 0x00001CBB
		protected internal virtual void RemoveNode(SiteMapNode node)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x0002AB50 File Offset: 0x00028D50
		protected SiteMapNode ResolveSiteMapNode(HttpContext context)
		{
			SiteMapResolveEventHandler siteMapResolve = this.SiteMapResolve;
			if (siteMapResolve == null)
			{
				return null;
			}
			if (!context.Items.Contains(this._resolutionTicket))
			{
				context.Items.Add(this._resolutionTicket, true);
				try
				{
					Delegate[] invocationList = siteMapResolve.GetInvocationList();
					int num = invocationList.Length;
					for (int i = 0; i < num; i++)
					{
						SiteMapNode siteMapNode = ((SiteMapResolveEventHandler)invocationList[i])(this, new SiteMapResolveEventArgs(context, this));
						if (siteMapNode != null)
						{
							return siteMapNode;
						}
					}
				}
				finally
				{
					context.Items.Remove(this._resolutionTicket);
				}
			}
			return null;
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x0002ABF4 File Offset: 0x00028DF4
		internal SiteMapNode ReturnNodeIfAccessible(SiteMapNode node)
		{
			if (node != null && node.IsAccessibleToUser(HttpContext.Current))
			{
				return node;
			}
			return null;
		}

		// Token: 0x040005BC RID: 1468
		private bool _securityTrimmingEnabled;

		// Token: 0x040005BD RID: 1469
		private bool _enableLocalization;

		// Token: 0x040005BE RID: 1470
		private string _resourceKey;

		// Token: 0x040005BF RID: 1471
		internal const string _securityTrimmingEnabledAttrName = "securityTrimmingEnabled";

		// Token: 0x040005C0 RID: 1472
		private const string _allRoles = "*";

		// Token: 0x040005C1 RID: 1473
		private SiteMapProvider _rootProvider;

		// Token: 0x040005C2 RID: 1474
		private SiteMapProvider _parentProvider;

		// Token: 0x040005C3 RID: 1475
		private object _resolutionTicket = new object();

		// Token: 0x040005C4 RID: 1476
		internal readonly object _lock = new object();
	}
}
