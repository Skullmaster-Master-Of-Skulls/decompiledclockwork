using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000738 RID: 1848
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class WebPartManagerInternals
	{
		// Token: 0x060059AB RID: 22955 RVA: 0x0016A538 File Offset: 0x00169538
		internal WebPartManagerInternals(WebPartManager manager)
		{
			this._manager = manager;
		}

		// Token: 0x060059AC RID: 22956 RVA: 0x0016A547 File Offset: 0x00169547
		public void AddWebPart(WebPart webPart)
		{
			this._manager.AddWebPart(webPart);
		}

		// Token: 0x060059AD RID: 22957 RVA: 0x0016A555 File Offset: 0x00169555
		public void CallOnClosing(WebPart webPart)
		{
			webPart.OnClosing(EventArgs.Empty);
		}

		// Token: 0x060059AE RID: 22958 RVA: 0x0016A562 File Offset: 0x00169562
		public void CallOnConnectModeChanged(WebPart webPart)
		{
			webPart.OnConnectModeChanged(EventArgs.Empty);
		}

		// Token: 0x060059AF RID: 22959 RVA: 0x0016A56F File Offset: 0x0016956F
		public void CallOnDeleting(WebPart webPart)
		{
			webPart.OnDeleting(EventArgs.Empty);
		}

		// Token: 0x060059B0 RID: 22960 RVA: 0x0016A57C File Offset: 0x0016957C
		public void CallOnEditModeChanged(WebPart webPart)
		{
			webPart.OnEditModeChanged(EventArgs.Empty);
		}

		// Token: 0x060059B1 RID: 22961 RVA: 0x0016A589 File Offset: 0x00169589
		public object CreateObjectFromType(Type type)
		{
			return WebPartUtil.CreateObjectFromType(type);
		}

		// Token: 0x060059B2 RID: 22962 RVA: 0x0016A591 File Offset: 0x00169591
		public bool ConnectionDeleted(WebPartConnection connection)
		{
			return connection.Deleted;
		}

		// Token: 0x060059B3 RID: 22963 RVA: 0x0016A599 File Offset: 0x00169599
		public void DeleteConnection(WebPartConnection connection)
		{
			connection.Deleted = true;
		}

		// Token: 0x060059B4 RID: 22964 RVA: 0x0016A5A2 File Offset: 0x001695A2
		public string GetZoneID(WebPart webPart)
		{
			return webPart.ZoneID;
		}

		// Token: 0x060059B5 RID: 22965 RVA: 0x0016A5AA File Offset: 0x001695AA
		public void LoadConfigurationState(WebPartTransformer transformer, object savedState)
		{
			transformer.LoadConfigurationState(savedState);
		}

		// Token: 0x060059B6 RID: 22966 RVA: 0x0016A5B3 File Offset: 0x001695B3
		public void RemoveWebPart(WebPart webPart)
		{
			this._manager.RemoveWebPart(webPart);
		}

		// Token: 0x060059B7 RID: 22967 RVA: 0x0016A5C1 File Offset: 0x001695C1
		public object SaveConfigurationState(WebPartTransformer transformer)
		{
			return transformer.SaveConfigurationState();
		}

		// Token: 0x060059B8 RID: 22968 RVA: 0x0016A5C9 File Offset: 0x001695C9
		public void SetConnectErrorMessage(WebPart webPart, string connectErrorMessage)
		{
			webPart.SetConnectErrorMessage(connectErrorMessage);
		}

		// Token: 0x060059B9 RID: 22969 RVA: 0x0016A5D2 File Offset: 0x001695D2
		public void SetHasUserData(WebPart webPart, bool hasUserData)
		{
			webPart.SetHasUserData(hasUserData);
		}

		// Token: 0x060059BA RID: 22970 RVA: 0x0016A5DB File Offset: 0x001695DB
		public void SetHasSharedData(WebPart webPart, bool hasSharedData)
		{
			webPart.SetHasSharedData(hasSharedData);
		}

		// Token: 0x060059BB RID: 22971 RVA: 0x0016A5E4 File Offset: 0x001695E4
		public void SetIsClosed(WebPart webPart, bool isClosed)
		{
			webPart.SetIsClosed(isClosed);
		}

		// Token: 0x060059BC RID: 22972 RVA: 0x0016A5ED File Offset: 0x001695ED
		public void SetIsShared(WebPartConnection connection, bool isShared)
		{
			connection.SetIsShared(isShared);
		}

		// Token: 0x060059BD RID: 22973 RVA: 0x0016A5F6 File Offset: 0x001695F6
		public void SetIsShared(WebPart webPart, bool isShared)
		{
			webPart.SetIsShared(isShared);
		}

		// Token: 0x060059BE RID: 22974 RVA: 0x0016A5FF File Offset: 0x001695FF
		public void SetIsStandalone(WebPart webPart, bool isStandalone)
		{
			webPart.SetIsStandalone(isStandalone);
		}

		// Token: 0x060059BF RID: 22975 RVA: 0x0016A608 File Offset: 0x00169608
		public void SetIsStatic(WebPartConnection connection, bool isStatic)
		{
			connection.SetIsStatic(isStatic);
		}

		// Token: 0x060059C0 RID: 22976 RVA: 0x0016A611 File Offset: 0x00169611
		public void SetIsStatic(WebPart webPart, bool isStatic)
		{
			webPart.SetIsStatic(isStatic);
		}

		// Token: 0x060059C1 RID: 22977 RVA: 0x0016A61A File Offset: 0x0016961A
		public void SetTransformer(WebPartConnection connection, WebPartTransformer transformer)
		{
			connection.SetTransformer(transformer);
		}

		// Token: 0x060059C2 RID: 22978 RVA: 0x0016A623 File Offset: 0x00169623
		public void SetZoneID(WebPart webPart, string zoneID)
		{
			webPart.ZoneID = zoneID;
		}

		// Token: 0x060059C3 RID: 22979 RVA: 0x0016A62C File Offset: 0x0016962C
		public void SetZoneIndex(WebPart webPart, int zoneIndex)
		{
			webPart.SetZoneIndex(zoneIndex);
		}

		// Token: 0x04003062 RID: 12386
		private WebPartManager _manager;
	}
}
