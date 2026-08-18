using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x020005A5 RID: 1445
	public sealed class WebPartManagerInternals
	{
		// Token: 0x0600491D RID: 18717 RVA: 0x000F3085 File Offset: 0x000F1285
		internal WebPartManagerInternals(WebPartManager manager)
		{
			this._manager = manager;
		}

		// Token: 0x0600491E RID: 18718 RVA: 0x000F3094 File Offset: 0x000F1294
		public void AddWebPart(WebPart webPart)
		{
			this._manager.AddWebPart(webPart);
		}

		// Token: 0x0600491F RID: 18719 RVA: 0x000F30A2 File Offset: 0x000F12A2
		public void CallOnClosing(WebPart webPart)
		{
			webPart.OnClosing(EventArgs.Empty);
		}

		// Token: 0x06004920 RID: 18720 RVA: 0x000F30AF File Offset: 0x000F12AF
		public void CallOnConnectModeChanged(WebPart webPart)
		{
			webPart.OnConnectModeChanged(EventArgs.Empty);
		}

		// Token: 0x06004921 RID: 18721 RVA: 0x000F30BC File Offset: 0x000F12BC
		public void CallOnDeleting(WebPart webPart)
		{
			webPart.OnDeleting(EventArgs.Empty);
		}

		// Token: 0x06004922 RID: 18722 RVA: 0x000F30C9 File Offset: 0x000F12C9
		public void CallOnEditModeChanged(WebPart webPart)
		{
			webPart.OnEditModeChanged(EventArgs.Empty);
		}

		// Token: 0x06004923 RID: 18723 RVA: 0x000F30D6 File Offset: 0x000F12D6
		public object CreateObjectFromType(Type type)
		{
			return WebPartUtil.CreateObjectFromType(type);
		}

		// Token: 0x06004924 RID: 18724 RVA: 0x000F30DE File Offset: 0x000F12DE
		public bool ConnectionDeleted(WebPartConnection connection)
		{
			return connection.Deleted;
		}

		// Token: 0x06004925 RID: 18725 RVA: 0x000F30E6 File Offset: 0x000F12E6
		public void DeleteConnection(WebPartConnection connection)
		{
			connection.Deleted = true;
		}

		// Token: 0x06004926 RID: 18726 RVA: 0x000F30EF File Offset: 0x000F12EF
		public string GetZoneID(WebPart webPart)
		{
			return webPart.ZoneID;
		}

		// Token: 0x06004927 RID: 18727 RVA: 0x000F30F7 File Offset: 0x000F12F7
		public void LoadConfigurationState(WebPartTransformer transformer, object savedState)
		{
			transformer.LoadConfigurationState(savedState);
		}

		// Token: 0x06004928 RID: 18728 RVA: 0x000F3100 File Offset: 0x000F1300
		public void RemoveWebPart(WebPart webPart)
		{
			this._manager.RemoveWebPart(webPart);
		}

		// Token: 0x06004929 RID: 18729 RVA: 0x000F310E File Offset: 0x000F130E
		public object SaveConfigurationState(WebPartTransformer transformer)
		{
			return transformer.SaveConfigurationState();
		}

		// Token: 0x0600492A RID: 18730 RVA: 0x000F3116 File Offset: 0x000F1316
		public void SetConnectErrorMessage(WebPart webPart, string connectErrorMessage)
		{
			webPart.SetConnectErrorMessage(connectErrorMessage);
		}

		// Token: 0x0600492B RID: 18731 RVA: 0x000F311F File Offset: 0x000F131F
		public void SetHasUserData(WebPart webPart, bool hasUserData)
		{
			webPart.SetHasUserData(hasUserData);
		}

		// Token: 0x0600492C RID: 18732 RVA: 0x000F3128 File Offset: 0x000F1328
		public void SetHasSharedData(WebPart webPart, bool hasSharedData)
		{
			webPart.SetHasSharedData(hasSharedData);
		}

		// Token: 0x0600492D RID: 18733 RVA: 0x000F3131 File Offset: 0x000F1331
		public void SetIsClosed(WebPart webPart, bool isClosed)
		{
			webPart.SetIsClosed(isClosed);
		}

		// Token: 0x0600492E RID: 18734 RVA: 0x000F313A File Offset: 0x000F133A
		public void SetIsShared(WebPartConnection connection, bool isShared)
		{
			connection.SetIsShared(isShared);
		}

		// Token: 0x0600492F RID: 18735 RVA: 0x000F3143 File Offset: 0x000F1343
		public void SetIsShared(WebPart webPart, bool isShared)
		{
			webPart.SetIsShared(isShared);
		}

		// Token: 0x06004930 RID: 18736 RVA: 0x000F314C File Offset: 0x000F134C
		public void SetIsStandalone(WebPart webPart, bool isStandalone)
		{
			webPart.SetIsStandalone(isStandalone);
		}

		// Token: 0x06004931 RID: 18737 RVA: 0x000F3155 File Offset: 0x000F1355
		public void SetIsStatic(WebPartConnection connection, bool isStatic)
		{
			connection.SetIsStatic(isStatic);
		}

		// Token: 0x06004932 RID: 18738 RVA: 0x000F315E File Offset: 0x000F135E
		public void SetIsStatic(WebPart webPart, bool isStatic)
		{
			webPart.SetIsStatic(isStatic);
		}

		// Token: 0x06004933 RID: 18739 RVA: 0x000F3167 File Offset: 0x000F1367
		public void SetTransformer(WebPartConnection connection, WebPartTransformer transformer)
		{
			connection.SetTransformer(transformer);
		}

		// Token: 0x06004934 RID: 18740 RVA: 0x000F3170 File Offset: 0x000F1370
		public void SetZoneID(WebPart webPart, string zoneID)
		{
			webPart.ZoneID = zoneID;
		}

		// Token: 0x06004935 RID: 18741 RVA: 0x000F3179 File Offset: 0x000F1379
		public void SetZoneIndex(WebPart webPart, int zoneIndex)
		{
			webPart.SetZoneIndex(zoneIndex);
		}

		// Token: 0x04002796 RID: 10134
		private WebPartManager _manager;
	}
}
