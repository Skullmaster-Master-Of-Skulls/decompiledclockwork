using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000E1 RID: 225
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class LoginStatusDesigner : CompositeControlDesigner
	{
		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x0002974C File Offset: 0x0002794C
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				DesignerActionListCollection designerActionListCollection = new DesignerActionListCollection();
				designerActionListCollection.AddRange(base.ActionLists);
				designerActionListCollection.Add(new LoginStatusDesigner.LoginStatusDesignerActionList(this));
				return designerActionListCollection;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000791 RID: 1937 RVA: 0x00003B0F File Offset: 0x00001D0F
		protected override bool UsePreviewControl
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0002977C File Offset: 0x0002797C
		public override string GetDesignTimeHtml()
		{
			IDictionary dictionary = new HybridDictionary(2);
			dictionary["LoggedIn"] = this._loggedIn;
			LoginStatus loginStatus = (LoginStatus)base.ViewControl;
			((IControlDesignerAccessor)loginStatus).SetDesignModeState(dictionary);
			if (this._loggedIn)
			{
				string text = loginStatus.LogoutText;
				bool flag = text == null || text.Length == 0 || text == " ";
				if (flag)
				{
					loginStatus.LogoutText = "[" + loginStatus.ID + "]";
				}
			}
			else
			{
				string text = loginStatus.LoginText;
				bool flag = text == null || text.Length == 0 || text == " ";
				if (flag)
				{
					loginStatus.LoginText = "[" + loginStatus.ID + "]";
				}
			}
			return base.GetDesignTimeHtml();
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x00029847 File Offset: 0x00027A47
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(LoginStatus));
			this._loginStatus = (LoginStatus)component;
			base.Initialize(component);
		}

		// Token: 0x04000488 RID: 1160
		private bool _loggedIn;

		// Token: 0x04000489 RID: 1161
		private LoginStatus _loginStatus;

		// Token: 0x02000409 RID: 1033
		private class LoginStatusDesignerActionList : DesignerActionList
		{
			// Token: 0x060027D4 RID: 10196 RVA: 0x000F44B0 File Offset: 0x000F26B0
			public LoginStatusDesignerActionList(LoginStatusDesigner designer) : base(designer.Component)
			{
				this._designer = designer;
			}

			// Token: 0x17000852 RID: 2130
			// (get) Token: 0x060027D5 RID: 10197 RVA: 0x00003B0F File Offset: 0x00001D0F
			// (set) Token: 0x060027D6 RID: 10198 RVA: 0x00003937 File Offset: 0x00001B37
			public override bool AutoShow
			{
				get
				{
					return true;
				}
				set
				{
				}
			}

			// Token: 0x17000853 RID: 2131
			// (get) Token: 0x060027D7 RID: 10199 RVA: 0x000F44C5 File Offset: 0x000F26C5
			// (set) Token: 0x060027D8 RID: 10200 RVA: 0x000F44EC File Offset: 0x000F26EC
			[TypeConverter(typeof(LoginStatusDesigner.LoginStatusDesignerActionList.LoginStatusViewTypeConverter))]
			public string View
			{
				get
				{
					if (this._designer._loggedIn)
					{
						return SR.GetString("LoginStatus_LoggedInView");
					}
					return SR.GetString("LoginStatus_LoggedOutView");
				}
				set
				{
					if (string.Compare(value, SR.GetString("LoginStatus_LoggedInView"), StringComparison.Ordinal) == 0)
					{
						this._designer._loggedIn = true;
					}
					else if (string.Compare(value, SR.GetString("LoginStatus_LoggedOutView"), StringComparison.Ordinal) == 0)
					{
						this._designer._loggedIn = false;
					}
					this._designer.UpdateDesignTimeHtml();
				}
			}

			// Token: 0x060027D9 RID: 10201 RVA: 0x000F4544 File Offset: 0x000F2744
			public override DesignerActionItemCollection GetSortedActionItems()
			{
				return new DesignerActionItemCollection
				{
					new DesignerActionPropertyItem("View", SR.GetString("WebControls_Views"), string.Empty, SR.GetString("WebControls_ViewsDescription"))
					{
						ShowInSourceView = false
					}
				};
			}

			// Token: 0x04001C74 RID: 7284
			private LoginStatusDesigner _designer;

			// Token: 0x020005C1 RID: 1473
			private class LoginStatusViewTypeConverter : TypeConverter
			{
				// Token: 0x060033F8 RID: 13304 RVA: 0x0011C0E0 File Offset: 0x0011A2E0
				public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
				{
					return new TypeConverter.StandardValuesCollection(new string[]
					{
						SR.GetString("LoginStatus_LoggedOutView"),
						SR.GetString("LoginStatus_LoggedInView")
					});
				}

				// Token: 0x060033F9 RID: 13305 RVA: 0x00003B0F File Offset: 0x00001D0F
				public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
				{
					return true;
				}

				// Token: 0x060033FA RID: 13306 RVA: 0x00003B0F File Offset: 0x00001D0F
				public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
				{
					return true;
				}
			}
		}
	}
}
