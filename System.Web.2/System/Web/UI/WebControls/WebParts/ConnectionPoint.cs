using System;
using System.Reflection;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000532 RID: 1330
	public abstract class ConnectionPoint
	{
		// Token: 0x06004379 RID: 17273 RVA: 0x000DE2C4 File Offset: 0x000DC4C4
		internal ConnectionPoint(MethodInfo callbackMethod, Type interfaceType, Type controlType, string displayName, string id, bool allowsMultipleConnections)
		{
			if (callbackMethod == null)
			{
				throw new ArgumentNullException("callbackMethod");
			}
			if (interfaceType == null)
			{
				throw new ArgumentNullException("interfaceType");
			}
			if (controlType == null)
			{
				throw new ArgumentNullException("controlType");
			}
			if (!controlType.IsSubclassOf(typeof(Control)))
			{
				throw new ArgumentException(SR.GetString("ConnectionPoint_InvalidControlType"), "controlType");
			}
			if (string.IsNullOrEmpty(displayName))
			{
				throw new ArgumentNullException("displayName");
			}
			this._callbackMethod = callbackMethod;
			this._interfaceType = interfaceType;
			this._controlType = controlType;
			this._displayName = displayName;
			this._id = id;
			this._allowsMultipleConnections = allowsMultipleConnections;
		}

		// Token: 0x170013C6 RID: 5062
		// (get) Token: 0x0600437A RID: 17274 RVA: 0x000DE37B File Offset: 0x000DC57B
		public bool AllowsMultipleConnections
		{
			get
			{
				return this._allowsMultipleConnections;
			}
		}

		// Token: 0x170013C7 RID: 5063
		// (get) Token: 0x0600437B RID: 17275 RVA: 0x000DE383 File Offset: 0x000DC583
		internal MethodInfo CallbackMethod
		{
			get
			{
				return this._callbackMethod;
			}
		}

		// Token: 0x170013C8 RID: 5064
		// (get) Token: 0x0600437C RID: 17276 RVA: 0x000DE38B File Offset: 0x000DC58B
		public Type ControlType
		{
			get
			{
				return this._controlType;
			}
		}

		// Token: 0x170013C9 RID: 5065
		// (get) Token: 0x0600437D RID: 17277 RVA: 0x000DE393 File Offset: 0x000DC593
		public Type InterfaceType
		{
			get
			{
				return this._interfaceType;
			}
		}

		// Token: 0x170013CA RID: 5066
		// (get) Token: 0x0600437E RID: 17278 RVA: 0x000DE39B File Offset: 0x000DC59B
		public string ID
		{
			get
			{
				if (string.IsNullOrEmpty(this._id))
				{
					return ConnectionPoint.DefaultID;
				}
				return this._id;
			}
		}

		// Token: 0x170013CB RID: 5067
		// (get) Token: 0x0600437F RID: 17279 RVA: 0x000DE3B6 File Offset: 0x000DC5B6
		public string DisplayName
		{
			get
			{
				return this._displayName;
			}
		}

		// Token: 0x06004380 RID: 17280 RVA: 0x000097B7 File Offset: 0x000079B7
		public virtual bool GetEnabled(Control control)
		{
			return true;
		}

		// Token: 0x040025E0 RID: 9696
		private MethodInfo _callbackMethod;

		// Token: 0x040025E1 RID: 9697
		private Type _controlType;

		// Token: 0x040025E2 RID: 9698
		private Type _interfaceType;

		// Token: 0x040025E3 RID: 9699
		private string _displayName;

		// Token: 0x040025E4 RID: 9700
		private string _id;

		// Token: 0x040025E5 RID: 9701
		private bool _allowsMultipleConnections;

		// Token: 0x040025E6 RID: 9702
		public static readonly string DefaultID = "default";

		// Token: 0x040025E7 RID: 9703
		internal const string DefaultIDInternal = "default";
	}
}
