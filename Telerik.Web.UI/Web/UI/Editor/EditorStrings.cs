using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x020012A1 RID: 4769
	[ParseChildren(true)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[PersistChildren(false)]
	public class EditorStrings : StateManager
	{
		// Token: 0x0600C7DA RID: 51162 RVA: 0x002C8044 File Offset: 0x002C6244
		internal EditorStrings(RadEditor editor)
		{
			this._tools = new ToolsStrings(new LocalizationProvider("RadEditor.Tools", editor, editor.LocalizationPath), false);
			this._main = new MainStrings(new LocalizationProvider("RadEditor.Main", editor, editor.LocalizationPath), false);
			this._modules = new ModulesStrings(new LocalizationProvider("RadEditor.Modules", editor, editor.LocalizationPath), false);
		}

		// Token: 0x17004092 RID: 16530
		// (get) Token: 0x0600C7DB RID: 51163 RVA: 0x002C80AE File Offset: 0x002C62AE
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ToolsStrings Tools
		{
			get
			{
				return this._tools;
			}
		}

		// Token: 0x17004093 RID: 16531
		// (get) Token: 0x0600C7DC RID: 51164 RVA: 0x002C80B6 File Offset: 0x002C62B6
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public MainStrings Main
		{
			get
			{
				return this._main;
			}
		}

		// Token: 0x17004094 RID: 16532
		// (get) Token: 0x0600C7DD RID: 51165 RVA: 0x002C80BE File Offset: 0x002C62BE
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ModulesStrings Modules
		{
			get
			{
				return this._modules;
			}
		}

		// Token: 0x0600C7DE RID: 51166 RVA: 0x002C80C8 File Offset: 0x002C62C8
		internal string Serialize(JavaScriptSerializer serializer)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (PropertyInfo propertyInfo in typeof(MainStrings).GetProperties())
			{
				dictionary[propertyInfo.Name.ToLowerInvariant()] = (string)propertyInfo.GetValue(this.Main, null);
			}
			foreach (PropertyInfo propertyInfo2 in typeof(ToolsStrings).GetProperties())
			{
				dictionary[propertyInfo2.Name.ToLowerInvariant()] = (string)propertyInfo2.GetValue(this.Tools, null);
			}
			foreach (PropertyInfo propertyInfo3 in typeof(ModulesStrings).GetProperties())
			{
				dictionary[propertyInfo3.Name.ToLowerInvariant()] = (string)propertyInfo3.GetValue(this.Modules, null);
			}
			return serializer.Serialize(dictionary);
		}

		// Token: 0x0600C7DF RID: 51167 RVA: 0x002C81CC File Offset: 0x002C63CC
		internal void addStrings(Dictionary<string, string> locStrings, string stringsName)
		{
			if (stringsName != null)
			{
				LocalizationStrings localizationStrings;
				if (!(stringsName == "Main"))
				{
					if (!(stringsName == "Tools"))
					{
						if (!(stringsName == "Modules"))
						{
							goto IL_49;
						}
						localizationStrings = this.Modules;
					}
					else
					{
						localizationStrings = this.Tools;
					}
				}
				else
				{
					localizationStrings = this.Main;
				}
				foreach (PropertyInfo propertyInfo in localizationStrings.GetType().GetProperties())
				{
					locStrings[propertyInfo.Name.ToLowerInvariant()] = (string)propertyInfo.GetValue(localizationStrings, null);
				}
				return;
			}
			IL_49:
			throw new ArgumentException("Unknown Localization Strings!");
		}

		// Token: 0x0600C7E0 RID: 51168 RVA: 0x002C8270 File Offset: 0x002C6470
		internal static Dictionary<string, string> getLightDialogsStrings(RadEditor editor)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			DialogsStrings dialogsStrings = new DialogsStrings(new LocalizationProvider("RadEditor.Dialogs", editor, editor.LocalizationPath), new string[]
			{
				"SetImageProperties",
				"LinkManager",
				"TableWizard"
			}, false);
			Type type = dialogsStrings.GetType();
			dictionary["ok"] = (string)type.GetProperty("Common_OK").GetValue(dialogsStrings, null);
			dictionary["cancel"] = (string)type.GetProperty("Common_Cancel").GetValue(dialogsStrings, null);
			dictionary["allproperties"] = (string)type.GetProperty("Common_AllProperties").GetValue(dialogsStrings, null);
			dictionary["linkurl"] = (string)type.GetProperty("LinkManager_LinkUrl").GetValue(dialogsStrings, null);
			dictionary["linktext"] = (string)type.GetProperty("LinkManager_LinkText").GetValue(dialogsStrings, null);
			dictionary["linktarget"] = (string)type.GetProperty("LinkManager_LinkTarget").GetValue(dialogsStrings, null);
			dictionary["presettargets"] = (string)type.GetProperty("Common_PresetTargets").GetValue(dialogsStrings, null);
			dictionary["none"] = (string)type.GetProperty("Common_None").GetValue(dialogsStrings, null);
			dictionary["targetself"] = (string)type.GetProperty("Common_TargetSelf").GetValue(dialogsStrings, null);
			dictionary["targetblank"] = (string)type.GetProperty("Common_TargetBlank").GetValue(dialogsStrings, null);
			dictionary["targetparent"] = (string)type.GetProperty("Common_TargetParent").GetValue(dialogsStrings, null);
			dictionary["targettop"] = (string)type.GetProperty("Common_TargetTop").GetValue(dialogsStrings, null);
			dictionary["targetsearch"] = (string)type.GetProperty("Common_TargetSearch").GetValue(dialogsStrings, null);
			dictionary["targetmedia"] = (string)type.GetProperty("Common_TargetMedia").GetValue(dialogsStrings, null);
			dictionary["customtargets"] = (string)type.GetProperty("Common_CustomTargets").GetValue(dialogsStrings, null);
			dictionary["addcustomtarget"] = (string)type.GetProperty("Common_AddCustomTarget").GetValue(dialogsStrings, null);
			dictionary["linkmanagertitle"] = (string)type.GetProperty("LinkManager_Title").GetValue(dialogsStrings, null);
			dictionary["width"] = (string)type.GetProperty("Common_Width").GetValue(dialogsStrings, null);
			dictionary["height"] = (string)type.GetProperty("Common_Height").GetValue(dialogsStrings, null);
			dictionary["imagealttext"] = (string)type.GetProperty("Common_ImageAltText").GetValue(dialogsStrings, null);
			dictionary["imagetitletext"] = (string)type.GetProperty("Common_ImageTitleText").GetValue(dialogsStrings, null);
			dictionary["imagesrc"] = (string)type.GetProperty("SetImageProperties_ImageSrc").GetValue(dialogsStrings, null);
			dictionary["columns"] = (string)type.GetProperty("TableWizard_Columns").GetValue(dialogsStrings, null);
			dictionary["rows"] = (string)type.GetProperty("TableWizard_Rows").GetValue(dialogsStrings, null);
			dictionary["alignment"] = (string)type.GetProperty("TableWizard_Alignment").GetValue(dialogsStrings, null);
			dictionary["cellpadding"] = (string)type.GetProperty("TableWizard_CellPadding").GetValue(dialogsStrings, null);
			dictionary["cellspacing"] = (string)type.GetProperty("TableWizard_CellSpacing").GetValue(dialogsStrings, null);
			dictionary["border"] = (string)type.GetProperty("TableWizard_Border").GetValue(dialogsStrings, null);
			dictionary["layout"] = (string)type.GetProperty("TableWizard_Layout").GetValue(dialogsStrings, null);
			return dictionary;
		}

		// Token: 0x0600C7E1 RID: 51169 RVA: 0x002C86A0 File Offset: 0x002C68A0
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.Tools).LoadViewState(array[1]);
			((IStateManager)this.Main).LoadViewState(array[2]);
			((IStateManager)this.Modules).LoadViewState(array[3]);
		}

		// Token: 0x0600C7E2 RID: 51170 RVA: 0x002C86E8 File Offset: 0x002C68E8
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.Tools).SaveViewState(),
				((IStateManager)this.Main).SaveViewState(),
				((IStateManager)this.Modules).SaveViewState()
			};
		}

		// Token: 0x0600C7E3 RID: 51171 RVA: 0x002C8732 File Offset: 0x002C6932
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Tools).TrackViewState();
			((IStateManager)this.Main).TrackViewState();
			((IStateManager)this.Modules).TrackViewState();
		}

		// Token: 0x04003491 RID: 13457
		private readonly ToolsStrings _tools;

		// Token: 0x04003492 RID: 13458
		private readonly MainStrings _main;

		// Token: 0x04003493 RID: 13459
		private readonly ModulesStrings _modules;
	}
}
