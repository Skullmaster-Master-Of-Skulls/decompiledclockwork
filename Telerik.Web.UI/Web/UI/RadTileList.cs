using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.UI.TileList;
using Telerik.Web.UI.TileList.Utils;

namespace Telerik.Web.UI
{
	// Token: 0x02000912 RID: 2322
	[AdaptiveRendering]
	[TelerikToolboxCategory("Data")]
	[ToolboxData("<{0}:RadTileList runat=server></{0}:RadTileList>")]
	[LightweightRendering]
	[EmbeddedSkin("TileList")]
	[EmbeddedSkin("TileList", "Default")]
	[ClientScriptResource("Telerik.Web.UI.RadTileList", "Telerik.Web.UI.TileList.RadTileListScripts.js")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadTileList))]
	[Description("Telerik RadTileList")]
	[Designer("Telerik.Web.Design.RadTileListDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ToolboxBitmap(typeof(RadTileList), "Telerik.Web.UI.TileList.png")]
	[RequiredScript(typeof(RadTileListScripts))]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	public class RadTileList : RadDataBoundControl, INamingContainer
	{
		// Token: 0x060057AC RID: 22444 RVA: 0x0010BC48 File Offset: 0x00109E48
		protected override void RegisterCssReferences()
		{
			base.RegisterCssReferences();
			if (this.GetAllTiles().Count == 0)
			{
				RadStyleSheetManager current = RadStyleSheetManager.GetCurrent(this.Page);
				RadTextTile radTextTile = new RadTextTile
				{
					Skin = base.RuntimeSkin
				};
				this.Controls.Add(radTextTile);
				if (current == null)
				{
					SkinRegistrar.RegisterCssReferences(radTextTile);
				}
				else
				{
					current.RegisterSkinnableControl(radTextTile);
				}
				this.Controls.Remove(radTextTile);
			}
		}

		// Token: 0x060057AE RID: 22446 RVA: 0x0010BCD0 File Offset: 0x00109ED0
		protected internal override string GetSkinSuffix()
		{
			string text = base.GetSkinSuffix();
			if (text == RenderModeHelper.GetRenderingModeString(RenderMode.Mobile))
			{
				text = RenderModeHelper.GetRenderingModeString(RenderMode.Lightweight);
			}
			return text;
		}

		// Token: 0x060057AF RID: 22447 RVA: 0x0010BCFC File Offset: 0x00109EFC
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			List<JavaScriptConverter> list = new List<JavaScriptConverter>();
			TileListBindingConverter tileListBindingConverter = new TileListBindingConverter();
			list.Add(tileListBindingConverter);
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(list);
			if (tileListBindingConverter.SerializeTileListBinding(this.DataBindings).Count > 0)
			{
				descriptor.AddScriptProperty("dataBindings", javaScriptSerializer.Serialize(this.DataBindings));
			}
			if (!string.IsNullOrEmpty(this.ClientDataSourceID))
			{
				try
				{
					Control control = DataSourceControlHelper.FindControl(this, this.ClientDataSourceID);
					descriptor.AddProperty("clientDataSourceID", control.ClientID);
				}
				catch (Exception)
				{
					descriptor.AddProperty("clientDataSourceID", this.ClientDataSourceID);
				}
			}
			base.DescribeRenderingMode(descriptor);
			this.DescribeGroups(descriptor, javaScriptSerializer);
			descriptor.AddProperty("skin", base.RuntimeSkin);
			descriptor.AddScriptProperty("_postBackReference", "\"" + this.GetPostbackEventReference() + "\"");
		}

		// Token: 0x060057B0 RID: 22448 RVA: 0x0010BDEC File Offset: 0x00109FEC
		private void DescribeGroups(IScriptDescriptor descriptor, JavaScriptSerializer serializer)
		{
			string[] array = new string[this.Groups.Count];
			string[] array2 = new string[this.Groups.Count];
			bool flag = false;
			bool flag2 = false;
			uint num = 0U;
			foreach (object obj in this.Groups)
			{
				TileGroup tileGroup = (TileGroup)obj;
				array[(int)((UIntPtr)num)] = tileGroup.Title;
				array2[(int)((UIntPtr)num)] = tileGroup.Name;
				if (!string.IsNullOrEmpty(tileGroup.Title))
				{
					flag = true;
				}
				if (!string.IsNullOrEmpty(tileGroup.Name))
				{
					flag2 = true;
				}
				num += 1U;
			}
			if (flag)
			{
				descriptor.AddProperty("groupTitles", array);
			}
			if (flag2)
			{
				descriptor.AddProperty("_groupNames", serializer.Serialize(array2));
			}
		}

		// Token: 0x060057B1 RID: 22449 RVA: 0x0010BED4 File Offset: 0x0010A0D4
		internal void Describe(IScriptDescriptor descriptor)
		{
			this.DescribeComponent(descriptor);
		}

		// Token: 0x060057B2 RID: 22450 RVA: 0x0010BEDD File Offset: 0x0010A0DD
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			base.RenderBeginTag(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtlistScrollWrapper rtlistHidden");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
		}

		// Token: 0x060057B3 RID: 22451 RVA: 0x0010BEFB File Offset: 0x0010A0FB
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			writer.RenderEndTag();
			base.RenderEndTag(writer);
		}

		// Token: 0x060057B4 RID: 22452 RVA: 0x0010BF20 File Offset: 0x0010A120
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.GetAllTiles().ForEach(delegate(RadBaseTile t)
			{
				t.RenderControl(writer);
			});
		}

		// Token: 0x060057B5 RID: 22453 RVA: 0x0010BF54 File Offset: 0x0010A154
		protected override void ControlPreRender()
		{
			bool flag = base.IsSkinSet || this.ViewState["EnableEmbeddedSkins"] != null || this.ViewState["EnableEmbeddedBaseStylesheet"] != null;
			int num = 0;
			foreach (object obj in this.Groups)
			{
				TileGroup tileGroup = (TileGroup)obj;
				foreach (object obj2 in tileGroup.Tiles)
				{
					RadBaseTile radBaseTile = (RadBaseTile)obj2;
					radBaseTile.GroupIndex = num;
					if (flag)
					{
						radBaseTile.Skin = SkinRegistrar.GetRuntimeSkin(this);
						radBaseTile.EnableEmbeddedBaseStylesheet = this.EnableEmbeddedBaseStylesheet;
						radBaseTile.EnableEmbeddedSkins = this.EnableEmbeddedSkins;
						radBaseTile.EnableAjaxSkinRendering = this.EnableAjaxSkinRendering;
					}
					radBaseTile.RenderMode = this.ResolvedRenderMode;
					this.AddTileToControls(radBaseTile);
				}
				num++;
			}
			base.ControlPreRender();
		}

		// Token: 0x060057B6 RID: 22454 RVA: 0x0010C088 File Offset: 0x0010A288
		public RadBaseTile GetTileByName(string name)
		{
			if (!string.IsNullOrEmpty(name))
			{
				foreach (RadBaseTile radBaseTile in this.GetAllTiles())
				{
					if (radBaseTile.Name == name)
					{
						return radBaseTile;
					}
				}
			}
			return null;
		}

		// Token: 0x060057B7 RID: 22455 RVA: 0x0010C0F4 File Offset: 0x0010A2F4
		public List<RadBaseTile> GetAllTiles()
		{
			List<RadBaseTile> list = new List<RadBaseTile>();
			foreach (object obj in this.Groups)
			{
				TileGroup tileGroup = (TileGroup)obj;
				list.AddRange(tileGroup.GetAllTiles());
			}
			return list;
		}

		// Token: 0x060057B8 RID: 22456 RVA: 0x0010C15C File Offset: 0x0010A35C
		public List<RadBaseTile> GetSelectedTiles()
		{
			List<RadBaseTile> list = new List<RadBaseTile>();
			foreach (object obj in this.Groups)
			{
				TileGroup tileGroup = (TileGroup)obj;
				list.AddRange(tileGroup.GetSelectedTiles());
			}
			return list;
		}

		// Token: 0x060057B9 RID: 22457 RVA: 0x0010C1CD File Offset: 0x0010A3CD
		public void ClearSelection()
		{
			this.GetSelectedTiles().ForEach(delegate(RadBaseTile t)
			{
				t.Selected = false;
			});
		}

		// Token: 0x060057BA RID: 22458 RVA: 0x0010C1F8 File Offset: 0x0010A3F8
		public TileGroup GetTileGroupByName(string name)
		{
			foreach (object obj in this.Groups)
			{
				TileGroup tileGroup = (TileGroup)obj;
				if (tileGroup.Name == name)
				{
					return tileGroup;
				}
			}
			return null;
		}

		// Token: 0x060057BB RID: 22459 RVA: 0x0010C260 File Offset: 0x0010A460
		internal void TileClickHandler(object sender, EventArgs e)
		{
			this.OnTileClick(new TileListEventArgs(sender as RadBaseTile));
		}

		// Token: 0x060057BC RID: 22460 RVA: 0x0010C274 File Offset: 0x0010A474
		internal void EnsureOnlyLastTileIsSelected()
		{
			if (this.SelectionMode == TileListSelectionMode.Single)
			{
				List<RadBaseTile> selectedTiles = this.GetSelectedTiles();
				bool flag = false;
				for (int i = selectedTiles.Count - 1; i >= 0; i--)
				{
					if (flag)
					{
						selectedTiles[i].Selected = false;
					}
					else
					{
						flag = true;
					}
				}
			}
		}

		// Token: 0x060057BD RID: 22461 RVA: 0x0010C2BC File Offset: 0x0010A4BC
		protected virtual string GetPostbackEventReference()
		{
			string postBackEventReference = this.Page.ClientScript.GetPostBackEventReference(this.GetPostBackOptions(this, "arguments", this.PostBackUrl));
			return postBackEventReference.Replace("\"", "'");
		}

		// Token: 0x060057BE RID: 22462 RVA: 0x0010C2FC File Offset: 0x0010A4FC
		internal PostBackOptions GetPostBackOptions(Control control, string argument, string postBackUrl)
		{
			PostBackOptions postBackOptions = new PostBackOptions(control, argument)
			{
				ClientSubmit = true
			};
			if (this.Page != null && !string.IsNullOrEmpty(postBackUrl))
			{
				postBackOptions.ActionUrl = postBackUrl;
			}
			return postBackOptions;
		}

		// Token: 0x060057BF RID: 22463 RVA: 0x0010C334 File Offset: 0x0010A534
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			this.EnsureChildControls();
			string text = postCollection[base.ClientStateFieldID];
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			RadTileListClientState radTileListClientState = null;
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			try
			{
				radTileListClientState = javaScriptSerializer.Deserialize<RadTileListClientState>(text);
			}
			catch (InvalidOperationException)
			{
			}
			catch (ArgumentException)
			{
			}
			if (radTileListClientState == null)
			{
				return false;
			}
			this._tileGroupIndices = radTileListClientState.TileGroupIndices;
			if (this.EnableDragAndDrop)
			{
				this.LoadGropusClientState(radTileListClientState);
			}
			return true;
		}

		// Token: 0x060057C0 RID: 22464 RVA: 0x0010C3B4 File Offset: 0x0010A5B4
		private void LoadGropusClientState(RadTileListClientState clientState)
		{
			this.StoreInitialGroupCollection();
			this.SetTilesGroupIdAndIndex();
			this.SetGroupsFromClientState(clientState, this._initialGroupsCollection);
		}

		// Token: 0x060057C1 RID: 22465 RVA: 0x0010C3D0 File Offset: 0x0010A5D0
		private void StoreInitialGroupCollection()
		{
			this._initialGroupsCollection = new TileGroupCollection(this);
			foreach (object obj in this.Groups)
			{
				TileGroup tileGroup = (TileGroup)obj;
				for (int i = tileGroup.Tiles.Count - 1; i >= 0; i--)
				{
					if (!tileGroup.Tiles[i].Visible)
					{
						tileGroup.Tiles.RemoveAt(i);
					}
				}
				this._initialGroupsCollection.Add(tileGroup);
			}
		}

		// Token: 0x060057C2 RID: 22466 RVA: 0x0010C474 File Offset: 0x0010A674
		private void SetTilesGroupIdAndIndex()
		{
			int num = 0;
			for (int i = 0; i < this._groups.Count; i++)
			{
				TileGroup tileGroup = this._groups[i];
				for (int j = 0; j < tileGroup.Tiles.Count; j++)
				{
					RadBaseTile radBaseTile = tileGroup.Tiles[j];
					radBaseTile.OriginalGroupIndex = j;
					radBaseTile.OriginalGroupId = i;
					radBaseTile.OriginalAllTilesIndex = num++;
				}
			}
		}

		// Token: 0x060057C3 RID: 22467 RVA: 0x0010C4E8 File Offset: 0x0010A6E8
		private void SetGroupsFromClientState(RadTileListClientState clientState, TileGroupCollection initialGroups)
		{
			TileGroupCollection tileGroupCollection = new TileGroupCollection(this);
			int num = 0;
			TileGroup tileGroup = new TileGroup();
			tileGroup.Title = this.GetTitleFromClientState(clientState, 0);
			tileGroup.Name = this.GetNameFromClientState(clientState, 0);
			tileGroupCollection.Add(tileGroup);
			for (int i = 0; i < clientState.TileGroupIndices.Count; i++)
			{
				object[] array = (clientState.TileGroupIndices[i] as ArrayList).ToArray();
				object obj = array[0];
				object obj2 = array[1];
				object obj3 = array[2];
				if (!obj.Equals(0) && !obj.Equals(num))
				{
					tileGroup = new TileGroup();
					tileGroupCollection.Add(tileGroup);
					num++;
					tileGroup.Title = this.GetTitleFromClientState(clientState, num);
					tileGroup.Name = this.GetNameFromClientState(clientState, num);
				}
				try
				{
					RadBaseTile item = initialGroups[int.Parse(obj2.ToString())].Tiles[int.Parse(obj3.ToString())];
					tileGroup.Tiles.Add(item);
				}
				catch (Exception)
				{
				}
			}
			this._groups = tileGroupCollection;
		}

		// Token: 0x060057C4 RID: 22468 RVA: 0x0010C60C File Offset: 0x0010A80C
		private string GetTitleFromClientState(RadTileListClientState clientState, int index)
		{
			if (clientState.TileGroupTitles == null)
			{
				return string.Empty;
			}
			string result;
			try
			{
				result = ((clientState.TileGroupTitles[index] as string) ?? string.Empty);
			}
			catch (IndexOutOfRangeException)
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x060057C5 RID: 22469 RVA: 0x0010C660 File Offset: 0x0010A860
		private string GetNameFromClientState(RadTileListClientState clientState, int index)
		{
			if (clientState.TileGroupNames == null)
			{
				return string.Empty;
			}
			string result;
			try
			{
				result = ((clientState.TileGroupNames[index] as string) ?? string.Empty);
			}
			catch (IndexOutOfRangeException)
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x060057C6 RID: 22470 RVA: 0x0010C6B4 File Offset: 0x0010A8B4
		protected override void RaisePostDataChangedEvent()
		{
			base.RaisePostDataChangedEvent();
			foreach (RadBaseTile radBaseTile in this.GetAllTiles())
			{
				if (radBaseTile._selectionStateChanged)
				{
					this._selectionStateChangedTiles.Add(radBaseTile);
				}
			}
			if (this._selectionStateChangedTiles.Count > 0)
			{
				this.OnSelectionChanged(new TileListDataEventArgs(this._selectionStateChangedTiles));
			}
		}

		// Token: 0x060057C7 RID: 22471 RVA: 0x0010C73C File Offset: 0x0010A93C
		private int GetCurrentGroupId(RadBaseTile tile)
		{
			int num = 0;
			foreach (object obj in this._groups)
			{
				TileGroup tileGroup = (TileGroup)obj;
				if (tileGroup.Tiles.IndexOf(tile) > -1)
				{
					return num;
				}
				num++;
			}
			return num;
		}

		// Token: 0x060057C8 RID: 22472 RVA: 0x0010C7B0 File Offset: 0x0010A9B0
		private List<string> GetSelectedTilesUniqueIds()
		{
			List<RadBaseTile> selectedTiles = this.GetSelectedTiles();
			List<string> list = new List<string>(selectedTiles.Count);
			foreach (RadBaseTile radBaseTile in selectedTiles)
			{
				list.Add(radBaseTile.UniqueID);
			}
			return list;
		}

		// Token: 0x060057C9 RID: 22473 RVA: 0x0010C83C File Offset: 0x0010AA3C
		private void SelectTilesByUniqueId(List<string> tilesUniqueIds)
		{
			this.ClearSelection();
			List<RadBaseTile> allTiles = this.GetAllTiles();
			allTiles.FindAll((RadBaseTile t) => tilesUniqueIds.Contains(t.UniqueID)).ForEach(delegate(RadBaseTile tile)
			{
				tile.Selected = true;
			});
		}

		// Token: 0x060057CA RID: 22474 RVA: 0x0010C897 File Offset: 0x0010AA97
		protected override void PerformDataBinding(IEnumerable data)
		{
			if (data == null && this.DataSource == null)
			{
				return;
			}
			this._dataBindingPerformed = true;
			if (!base.DesignMode)
			{
				this.PrepareForDataBinding();
				this.BindToEnumerableData(data);
				this.SetTilesGroupIdAndIndex();
			}
		}

		// Token: 0x060057CB RID: 22475 RVA: 0x0010C8C7 File Offset: 0x0010AAC7
		protected void PrepareForDataBinding()
		{
			if (!this.AppendDataBoundItems)
			{
				this.ClearEachGroup();
				base.ClearChildViewState();
			}
			this.TrackViewState();
		}

		// Token: 0x060057CC RID: 22476 RVA: 0x0010C8E4 File Offset: 0x0010AAE4
		private void ClearEachGroup()
		{
			this.Controls.Clear();
			foreach (object obj in this.Groups)
			{
				TileGroup tileGroup = (TileGroup)obj;
				tileGroup.Tiles.Clear();
			}
		}

		// Token: 0x060057CD RID: 22477 RVA: 0x0010C94C File Offset: 0x0010AB4C
		protected void BindToEnumerableData(IEnumerable dataSource)
		{
			this.CreateChildControls(dataSource, true);
			this.ViewState["_!DataBoundTilesDataSource"] = this._dataBoundTilesDataSource;
		}

		// Token: 0x060057CE RID: 22478 RVA: 0x0010C96C File Offset: 0x0010AB6C
		protected override void CreateChildControls()
		{
			if (this.ViewState["_!DataBoundTilesDataSource"] != null && !this._dataBindingPerformed)
			{
				if (!this.AppendDataBoundItems)
				{
					this.ClearEachGroup();
				}
				this.CreateChildControls(this.ViewState["_!DataBoundTilesDataSource"] as ArrayList, false);
			}
			this.AppendTilesToControlsCollection();
			base.CreateChildControls();
		}

		// Token: 0x060057CF RID: 22479 RVA: 0x0010C9CC File Offset: 0x0010ABCC
		private void AppendTilesToControlsCollection()
		{
			foreach (RadBaseTile tile in this.GetAllTiles())
			{
				this.AddTileToControls(tile);
			}
		}

		// Token: 0x060057D0 RID: 22480 RVA: 0x0010CA20 File Offset: 0x0010AC20
		private void AddTileToControls(RadBaseTile tile)
		{
			if (!this.Controls.Contains(tile))
			{
				this.Controls.Add(tile);
			}
		}

		// Token: 0x060057D1 RID: 22481 RVA: 0x0010CA3C File Offset: 0x0010AC3C
		protected void CreateChildControls(IEnumerable dataSource, bool dataBinding)
		{
			this.RemoveNotDeclarativeTiles();
			foreach (object obj in dataSource)
			{
				if (dataBinding)
				{
					RadBaseTile radBaseTile = this.CreateTile(obj);
					this.OnTileCreated(new TileListEventArgs(radBaseTile));
					string groupName = this.GetGroupName(obj);
					TileGroup orCreateGroupByName = this.GetOrCreateGroupByName(groupName);
					orCreateGroupByName.Tiles.Add(radBaseTile);
					this.RaiseTileDataBound(radBaseTile, obj);
					this._dataBoundTilesDataSource.Add(new Pair(groupName, radBaseTile.GetType().Name));
					radBaseTile.SetDirty();
				}
				else
				{
					Pair pair = obj as Pair;
					if (pair != null)
					{
						TileListTileType tileType = (TileListTileType)Enum.Parse(typeof(TileListTileType), (string)pair.Second);
						TileFactory tileFactory = new TileFactory(this.DataBindings, null);
						RadBaseTile radBaseTile2 = tileFactory.CreateEmptyTile(tileType);
						this.OnTileCreated(new TileListEventArgs(radBaseTile2));
						TileGroup orCreateGroupByName2 = this.GetOrCreateGroupByName((string)pair.First);
						orCreateGroupByName2.Tiles.Add(radBaseTile2);
					}
				}
			}
		}

		// Token: 0x060057D2 RID: 22482 RVA: 0x0010CBA4 File Offset: 0x0010ADA4
		private void RemoveNotDeclarativeTiles()
		{
			using (IEnumerator enumerator = this.Groups.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TileGroup group = (TileGroup)enumerator.Current;
					group.GetAllTiles().ForEach(delegate(RadBaseTile t)
					{
						if (!t.IsDeclarative)
						{
							this.Controls.Remove(t);
							group.Tiles.Remove(t);
						}
					});
				}
			}
		}

		// Token: 0x060057D3 RID: 22483 RVA: 0x0010CC2C File Offset: 0x0010AE2C
		private RadBaseTile CreateTile(object dataItem)
		{
			TileFactory tileFactory = new TileFactory(this.DataBindings, dataItem);
			return tileFactory.CreateTile();
		}

		// Token: 0x060057D4 RID: 22484 RVA: 0x0010CC4E File Offset: 0x0010AE4E
		private void RaiseTileDataBound(RadBaseTile tile, object dataItem)
		{
			tile.DataItem = dataItem;
			this.OnTileDataBound(new TileListEventArgs(tile));
			tile.DataItem = null;
		}

		// Token: 0x060057D5 RID: 22485 RVA: 0x0010CC6C File Offset: 0x0010AE6C
		private TileGroup GetOrCreateGroupByName(string groupName)
		{
			TileGroup tileGroup = this.GetTileGroupByName(groupName);
			if (tileGroup == null)
			{
				tileGroup = new TileGroup
				{
					Name = groupName
				};
				this.Groups.Add(tileGroup);
			}
			else if (tileGroup.TileList == null)
			{
				tileGroup.TileList = this;
			}
			return tileGroup;
		}

		// Token: 0x060057D6 RID: 22486 RVA: 0x0010CCB4 File Offset: 0x0010AEB4
		private string GetGroupName(object dataItem)
		{
			string result = "";
			string dataGroupNameField = this.DataBindings.CommonTileBinding.DataGroupNameField;
			if (!string.IsNullOrEmpty(dataGroupNameField))
			{
				result = RadTileList.GetValueFromDataItem(dataItem, dataGroupNameField);
			}
			return result;
		}

		// Token: 0x060057D7 RID: 22487 RVA: 0x0010CCEC File Offset: 0x0010AEEC
		internal static string GetValueFromDataItem(object dataItem, string dataValueField)
		{
			string result = null;
			if (!string.IsNullOrEmpty(dataValueField))
			{
				result = DataBinder.GetPropertyValue(dataItem, dataValueField, null);
			}
			return result;
		}

		// Token: 0x060057D8 RID: 22488 RVA: 0x0010CD10 File Offset: 0x0010AF10
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)this.DataBindings).LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				((IStateManager)this.Groups).LoadViewState(array[2]);
			}
			foreach (object obj in this.Groups)
			{
				TileGroup tileGroup = (TileGroup)obj;
				tileGroup.TileList = this;
			}
		}

		// Token: 0x060057D9 RID: 22489 RVA: 0x0010CDA0 File Offset: 0x0010AFA0
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.DataBindings).SaveViewState(),
				((IStateManager)this.Groups).SaveViewState()
			};
		}

		// Token: 0x060057DA RID: 22490 RVA: 0x0010CDDA File Offset: 0x0010AFDA
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.DataBindings).TrackViewState();
			((IStateManager)this.Groups).TrackViewState();
		}

		// Token: 0x17001D02 RID: 7426
		// (get) Token: 0x060057DB RID: 22491 RVA: 0x0010CDF8 File Offset: 0x0010AFF8
		protected override string CssClassFormatString
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("RadTileList RadTileList_{0}");
				if (this.ResolvedRenderMode == RenderMode.Mobile)
				{
					stringBuilder.Append(" rtlistResponsive");
				}
				if (this.ScrollingMode == TileListScrollingMode.None || this.ScrollingMode == TileListScrollingMode.Accelerated)
				{
					stringBuilder.Append(" rtlistScrollHidden");
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x17001D03 RID: 7427
		// (get) Token: 0x060057DC RID: 22492 RVA: 0x0010CE50 File Offset: 0x0010B050
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17001D04 RID: 7428
		// (get) Token: 0x060057DD RID: 22493 RVA: 0x0010CE54 File Offset: 0x0010B054
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001D05 RID: 7429
		// (get) Token: 0x060057DE RID: 22494 RVA: 0x0010CE58 File Offset: 0x0010B058
		// (set) Token: 0x060057DF RID: 22495 RVA: 0x0010CE8B File Offset: 0x0010B08B
		[DefaultValue(typeof(Unit), "")]
		[ClientControlProperty]
		[ClientPropertyName("height")]
		[NotifyParentProperty(true)]
		public override Unit Height
		{
			get
			{
				if (!base.Height.IsEmpty)
				{
					return base.Height;
				}
				return Unit.Parse("", CultureInfo.InvariantCulture);
			}
			set
			{
				base.Height = value;
			}
		}

		// Token: 0x17001D06 RID: 7430
		// (get) Token: 0x060057E0 RID: 22496 RVA: 0x0010CE94 File Offset: 0x0010B094
		// (set) Token: 0x060057E1 RID: 22497 RVA: 0x0010CEC7 File Offset: 0x0010B0C7
		[DefaultValue(typeof(Unit), "")]
		[ClientControlProperty]
		[ClientPropertyName("width")]
		[NotifyParentProperty(true)]
		public override Unit Width
		{
			get
			{
				if (!base.Width.IsEmpty)
				{
					return base.Width;
				}
				return Unit.Parse("", CultureInfo.InvariantCulture);
			}
			set
			{
				base.Width = value;
			}
		}

		// Token: 0x17001D07 RID: 7431
		// (get) Token: 0x060057E2 RID: 22498 RVA: 0x0010CED0 File Offset: 0x0010B0D0
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TileGroupCollection Groups
		{
			get
			{
				if (this._groups == null)
				{
					this._groups = new TileGroupCollection(this);
				}
				return this._groups;
			}
		}

		// Token: 0x17001D08 RID: 7432
		// (get) Token: 0x060057E3 RID: 22499 RVA: 0x0010CEEC File Offset: 0x0010B0EC
		// (set) Token: 0x060057E4 RID: 22500 RVA: 0x0010CEFA File Offset: 0x0010B0FA
		[DefaultValue(0)]
		[Description("Gets or sets in how many rows the tiles will be ordered.")]
		[ClientControlProperty]
		[Category("Behavior")]
		public int TileRows
		{
			get
			{
				return base.GetViewStateValue<int>("TileRows", 0);
			}
			set
			{
				this.ViewState["TileRows"] = value;
			}
		}

		// Token: 0x17001D09 RID: 7433
		// (get) Token: 0x060057E5 RID: 22501 RVA: 0x0010CF12 File Offset: 0x0010B112
		// (set) Token: 0x060057E6 RID: 22502 RVA: 0x0010CF20 File Offset: 0x0010B120
		[MergableProperty(true)]
		[Description("Gets or sets the value indicating the TileList selection mode, giving the tiles ability to be selected on context menu click.")]
		[Bindable(true)]
		[DefaultValue(TileListScrollingMode.Auto)]
		[ClientControlProperty]
		[Category("Behavior")]
		public TileListScrollingMode ScrollingMode
		{
			get
			{
				return base.GetViewStateValue<TileListScrollingMode>("ScrollingMode", TileListScrollingMode.Auto);
			}
			set
			{
				this.ViewState["ScrollingMode"] = value;
			}
		}

		// Token: 0x17001D0A RID: 7434
		// (get) Token: 0x060057E7 RID: 22503 RVA: 0x0010CF38 File Offset: 0x0010B138
		// (set) Token: 0x060057E8 RID: 22504 RVA: 0x0010CF48 File Offset: 0x0010B148
		[ClientControlProperty]
		[DefaultValue(TileListSelectionMode.None)]
		[Description("Gets or sets the value indicating the TileList selection mode, giving the tiles ability to be selected on context menu click.")]
		[Bindable(true)]
		[MergableProperty(true)]
		[Category("Behavior")]
		public TileListSelectionMode SelectionMode
		{
			get
			{
				return base.GetViewStateValue<TileListSelectionMode>("SelectionMode", TileListSelectionMode.None);
			}
			set
			{
				this.ViewState["SelectionMode"] = value;
				List<RadBaseTile> allTiles = this.GetAllTiles();
				bool flag = false;
				for (int i = allTiles.Count - 1; i >= 0; i--)
				{
					RadBaseTile radBaseTile = allTiles[i];
					if (value != TileListSelectionMode.None)
					{
						radBaseTile.EnableSelection = true;
						if (value == TileListSelectionMode.Single)
						{
							if (!flag && radBaseTile.Selected)
							{
								flag = true;
							}
							else if (radBaseTile.Selected)
							{
								radBaseTile.Selected = false;
								radBaseTile._selectionStateChanged = true;
							}
						}
					}
					else
					{
						if (radBaseTile.Selected)
						{
							radBaseTile.Selected = false;
							radBaseTile._selectionStateChanged = true;
						}
						radBaseTile.EnableSelection = false;
					}
				}
			}
		}

		// Token: 0x17001D0B RID: 7435
		// (get) Token: 0x060057E9 RID: 22505 RVA: 0x0010CFE2 File Offset: 0x0010B1E2
		// (set) Token: 0x060057EA RID: 22506 RVA: 0x0010D003 File Offset: 0x0010B203
		[Description("Whether to postback after the selection changes")]
		[ClientControlProperty]
		[Bindable(false)]
		[Category("Behavior")]
		[DefaultValue(false)]
		public bool AutoPostBack
		{
			get
			{
				return (bool)(this.ViewState["AutoPostBack"] ?? false);
			}
			set
			{
				this.ViewState["AutoPostBack"] = value;
			}
		}

		// Token: 0x17001D0C RID: 7436
		// (get) Token: 0x060057EB RID: 22507 RVA: 0x0010D01B File Offset: 0x0010B21B
		// (set) Token: 0x060057EC RID: 22508 RVA: 0x0010D03C File Offset: 0x0010B23C
		[DefaultValue(false)]
		[Description("Gets or sets a value indicating whether a drag and drop functionality is enabled")]
		[Bindable(false)]
		[ClientControlProperty]
		[Category("Behavior")]
		public bool EnableDragAndDrop
		{
			get
			{
				return (bool)(this.ViewState["EnableDragAndDrop"] ?? false);
			}
			set
			{
				this.ViewState["EnableDragAndDrop"] = value;
			}
		}

		// Token: 0x17001D0D RID: 7437
		// (get) Token: 0x060057ED RID: 22509 RVA: 0x0010D054 File Offset: 0x0010B254
		// (set) Token: 0x060057EE RID: 22510 RVA: 0x0010D074 File Offset: 0x0010B274
		[UrlProperty("*.aspx")]
		[Category("Behavior")]
		[DefaultValue("")]
		[Themeable(false)]
		public virtual string PostBackUrl
		{
			get
			{
				return (string)(this.ViewState["PostBackUrl"] ?? "");
			}
			set
			{
				this.ViewState["PostBackUrl"] = value;
			}
		}

		// Token: 0x17001D0E RID: 7438
		// (get) Token: 0x060057EF RID: 22511 RVA: 0x0010D087 File Offset: 0x0010B287
		// (set) Token: 0x060057F0 RID: 22512 RVA: 0x0010D0A2 File Offset: 0x0010B2A2
		[Category("Behavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		public TileListBinding DataBindings
		{
			get
			{
				if (this._dataBindings == null)
				{
					this._dataBindings = new TileListBinding();
				}
				return this._dataBindings;
			}
			set
			{
				this._dataBindings = value;
			}
		}

		// Token: 0x17001D0F RID: 7439
		// (get) Token: 0x060057F1 RID: 22513 RVA: 0x0010D0AB File Offset: 0x0010B2AB
		// (set) Token: 0x060057F2 RID: 22514 RVA: 0x0010D0CC File Offset: 0x0010B2CC
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("Gets or sets a bool value that indicates whether the tiles are cleared before data binding.")]
		public bool AppendDataBoundItems
		{
			get
			{
				return (bool)(this.ViewState["AppendDataBoundItems"] ?? false);
			}
			set
			{
				this.ViewState["AppendDataBoundItems"] = value;
			}
		}

		// Token: 0x17001D10 RID: 7440
		// (get) Token: 0x060057F3 RID: 22515 RVA: 0x0010D0E4 File Offset: 0x0010B2E4
		// (set) Token: 0x060057F4 RID: 22516 RVA: 0x0010D105 File Offset: 0x0010B305
		[ClientControlProperty]
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("Gets or sets a bool value that indicates whether all existing tiles are deleted before client data binding.")]
		public bool AppendClientDataBoundItems
		{
			get
			{
				return (bool)(this.ViewState["AppendClientDataBoundItems"] ?? false);
			}
			set
			{
				this.ViewState["AppendClientDataBoundItems"] = value;
			}
		}

		// Token: 0x140000D0 RID: 208
		// (add) Token: 0x060057F5 RID: 22517 RVA: 0x0010D11D File Offset: 0x0010B31D
		// (remove) Token: 0x060057F6 RID: 22518 RVA: 0x0010D130 File Offset: 0x0010B330
		[Description("Adds or removes an event handler method from the TileClick event.")]
		[Category("Action")]
		public event TileListEventHandler TileClick
		{
			add
			{
				base.Events.AddHandler(RadTileList.tileClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTileList.tileClickEvent, value);
			}
		}

		// Token: 0x060057F7 RID: 22519 RVA: 0x0010D144 File Offset: 0x0010B344
		protected virtual void OnTileClick(TileListEventArgs e)
		{
			TileListEventHandler tileListEventHandler = (TileListEventHandler)base.Events[RadTileList.tileClickEvent];
			if (tileListEventHandler != null)
			{
				tileListEventHandler(this, e);
			}
		}

		// Token: 0x140000D1 RID: 209
		// (add) Token: 0x060057F8 RID: 22520 RVA: 0x0010D172 File Offset: 0x0010B372
		// (remove) Token: 0x060057F9 RID: 22521 RVA: 0x0010D185 File Offset: 0x0010B385
		[Category("Action")]
		[Description("Adds or removes an event handler method from the TileDataBound event.")]
		public event TileListEventHandler TileDataBound
		{
			add
			{
				base.Events.AddHandler(RadTileList.tileDataBoundEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTileList.tileDataBoundEvent, value);
			}
		}

		// Token: 0x060057FA RID: 22522 RVA: 0x0010D198 File Offset: 0x0010B398
		protected virtual void OnTileDataBound(TileListEventArgs e)
		{
			TileListEventHandler tileListEventHandler = (TileListEventHandler)base.Events[RadTileList.tileDataBoundEvent];
			if (tileListEventHandler != null)
			{
				tileListEventHandler(this, e);
			}
		}

		// Token: 0x140000D2 RID: 210
		// (add) Token: 0x060057FB RID: 22523 RVA: 0x0010D1C6 File Offset: 0x0010B3C6
		// (remove) Token: 0x060057FC RID: 22524 RVA: 0x0010D1D9 File Offset: 0x0010B3D9
		[Description("Adds or removes an event handler method from the TileCreated event.")]
		[Category("Action")]
		public event TileListEventHandler TileCreated
		{
			add
			{
				base.Events.AddHandler(RadTileList.tileCreatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTileList.tileCreatedEvent, value);
			}
		}

		// Token: 0x060057FD RID: 22525 RVA: 0x0010D1EC File Offset: 0x0010B3EC
		protected virtual void OnTileCreated(TileListEventArgs e)
		{
			TileListEventHandler tileListEventHandler = (TileListEventHandler)base.Events[RadTileList.tileCreatedEvent];
			if (tileListEventHandler != null)
			{
				tileListEventHandler(this, e);
			}
		}

		// Token: 0x140000D3 RID: 211
		// (add) Token: 0x060057FE RID: 22526 RVA: 0x0010D21A File Offset: 0x0010B41A
		// (remove) Token: 0x060057FF RID: 22527 RVA: 0x0010D22D File Offset: 0x0010B42D
		public event TileListDataEventHandler SelectionChanged
		{
			add
			{
				base.Events.AddHandler(RadTileList.selectionChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTileList.selectionChangedEvent, value);
			}
		}

		// Token: 0x06005800 RID: 22528 RVA: 0x0010D240 File Offset: 0x0010B440
		protected virtual void OnSelectionChanged(TileListDataEventArgs e)
		{
			TileListDataEventHandler tileListDataEventHandler = (TileListDataEventHandler)base.Events[RadTileList.selectionChangedEvent];
			if (tileListDataEventHandler != null)
			{
				tileListDataEventHandler(this, e);
			}
		}

		// Token: 0x17001D11 RID: 7441
		// (get) Token: 0x06005801 RID: 22529 RVA: 0x0010D26E File Offset: 0x0010B46E
		// (set) Token: 0x06005802 RID: 22530 RVA: 0x0010D28E File Offset: 0x0010B48E
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("load")]
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientControlEvent]
		public string OnClientLoad
		{
			get
			{
				return (string)(this.ViewState["OnClientLoad"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientLoad"] = value;
			}
		}

		// Token: 0x17001D12 RID: 7442
		// (get) Token: 0x06005803 RID: 22531 RVA: 0x0010D2A1 File Offset: 0x0010B4A1
		// (set) Token: 0x06005804 RID: 22532 RVA: 0x0010D2C1 File Offset: 0x0010B4C1
		[Category("Client-side events")]
		[Description("The JavaScript function executed before a tile is selected")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("tileSelecting")]
		public string OnClientTileSelecting
		{
			get
			{
				return (string)(this.ViewState["OnClientTileSelecting"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientTileSelecting"] = value;
			}
		}

		// Token: 0x17001D13 RID: 7443
		// (get) Token: 0x06005805 RID: 22533 RVA: 0x0010D2D4 File Offset: 0x0010B4D4
		// (set) Token: 0x06005806 RID: 22534 RVA: 0x0010D2F4 File Offset: 0x0010B4F4
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[Description("The JavaScript function executed after a tile is selected")]
		[DefaultValue("")]
		[ClientControlEvent]
		[ClientPropertyName("tileSelected")]
		public string OnClientTileSelected
		{
			get
			{
				return (string)(this.ViewState["OnClientTileSelected"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientTileSelected"] = value;
			}
		}

		// Token: 0x17001D14 RID: 7444
		// (get) Token: 0x06005807 RID: 22535 RVA: 0x0010D307 File Offset: 0x0010B507
		// (set) Token: 0x06005808 RID: 22536 RVA: 0x0010D327 File Offset: 0x0010B527
		[ClientPropertyName("tileClicking")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[ClientControlEvent]
		[Category("Client-side events")]
		[Description("Gets or sets the name of the JavaScript function that will be called when a tile in a RadTileList is clicked. The event is cancelable.")]
		public string OnClientTileClicking
		{
			get
			{
				return ((string)this.ViewState["OnClientTileClicking"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientTileClicking"] = value;
			}
		}

		// Token: 0x17001D15 RID: 7445
		// (get) Token: 0x06005809 RID: 22537 RVA: 0x0010D33A File Offset: 0x0010B53A
		// (set) Token: 0x0600580A RID: 22538 RVA: 0x0010D35A File Offset: 0x0010B55A
		[DefaultValue("")]
		[ClientPropertyName("tileClicked")]
		[ClientControlEvent]
		[Description("Gets or sets the name of the JavaScript function that will be called when a tile in a RadTileList is clicked, after the OnClientClicking event.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		public string OnClientTileClicked
		{
			get
			{
				return ((string)this.ViewState["OnClientTileClicked"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientTileClicked"] = value;
			}
		}

		// Token: 0x17001D16 RID: 7446
		// (get) Token: 0x0600580B RID: 22539 RVA: 0x0010D36D File Offset: 0x0010B56D
		// (set) Token: 0x0600580C RID: 22540 RVA: 0x0010D38D File Offset: 0x0010B58D
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("tileDragStart")]
		[Description("Gets or sets the name of the JavaScript function that will be called before a tile dragging starts.")]
		[DefaultValue("")]
		[ClientControlEvent]
		[Category("Client-side events")]
		public string OnClientTileDragStart
		{
			get
			{
				return ((string)this.ViewState["OnClientTileDragStart"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientTileDragStart"] = value;
			}
		}

		// Token: 0x17001D17 RID: 7447
		// (get) Token: 0x0600580D RID: 22541 RVA: 0x0010D3A0 File Offset: 0x0010B5A0
		// (set) Token: 0x0600580E RID: 22542 RVA: 0x0010D3C0 File Offset: 0x0010B5C0
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("Gets or sets the name of the JavaScript function that will be called, when a tile is dragged.")]
		[ClientPropertyName("tileDragging")]
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientControlEvent]
		public string OnClientTileDragging
		{
			get
			{
				return ((string)this.ViewState["OnClientTileDragging"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientTileDragging"] = value;
			}
		}

		// Token: 0x17001D18 RID: 7448
		// (get) Token: 0x0600580F RID: 22543 RVA: 0x0010D3D3 File Offset: 0x0010B5D3
		// (set) Token: 0x06005810 RID: 22544 RVA: 0x0010D3F3 File Offset: 0x0010B5F3
		[ClientPropertyName("tileDropping")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[ClientControlEvent]
		[Category("Client-side events")]
		[Description("Gets or sets the name of the JavaScript function that will be called, before a tile is dropped.")]
		public string OnClientTileDropping
		{
			get
			{
				return ((string)this.ViewState["OnClientTileDropping"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientTileDropping"] = value;
			}
		}

		// Token: 0x17001D19 RID: 7449
		// (get) Token: 0x06005811 RID: 22545 RVA: 0x0010D406 File Offset: 0x0010B606
		// (set) Token: 0x06005812 RID: 22546 RVA: 0x0010D426 File Offset: 0x0010B626
		[Category("Client-side events")]
		[ClientPropertyName("tileDropped")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("Gets or sets the name of the JavaScript function that will be called, after a tile is dropped.")]
		[DefaultValue("")]
		[ClientControlEvent]
		public string OnClientTileDropped
		{
			get
			{
				return ((string)this.ViewState["OnClientTileDropped"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientTileDropped"] = value;
			}
		}

		// Token: 0x17001D1A RID: 7450
		// (get) Token: 0x06005813 RID: 22547 RVA: 0x0010D439 File Offset: 0x0010B639
		// (set) Token: 0x06005814 RID: 22548 RVA: 0x0010D459 File Offset: 0x0010B659
		[Category("Client-side events")]
		[ClientPropertyName("tileCreating")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("Gets or sets the name of the JavaScript function that will be called, before a tile is created. The event is cancelable.")]
		[DefaultValue("")]
		[ClientControlEvent]
		public string OnClientTileCreating
		{
			get
			{
				return ((string)this.ViewState["OnClientTileCreating"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientTileCreating"] = value;
			}
		}

		// Token: 0x17001D1B RID: 7451
		// (get) Token: 0x06005815 RID: 22549 RVA: 0x0010D46C File Offset: 0x0010B66C
		// (set) Token: 0x06005816 RID: 22550 RVA: 0x0010D48C File Offset: 0x0010B68C
		[DefaultValue("")]
		[Description("Gets or sets the name of the JavaScript function that will be called, after a tile is databound.")]
		[ClientPropertyName("tileDataBound")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Category("Client-side events")]
		public string OnClientTileDataBound
		{
			get
			{
				return ((string)this.ViewState["OnClientTileDataBound"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientTileDataBound"] = value;
			}
		}

		// Token: 0x17001D1C RID: 7452
		// (get) Token: 0x06005817 RID: 22551 RVA: 0x0010D49F File Offset: 0x0010B69F
		// (set) Token: 0x06005818 RID: 22552 RVA: 0x0010D4BF File Offset: 0x0010B6BF
		[Description("Gets or sets the name of the JavaScript function that will be called, after the TileList is databound.")]
		[ClientPropertyName("tileListDataBound")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Category("Client-side events")]
		public string OnClientTileListDataBound
		{
			get
			{
				return ((string)this.ViewState["OnClientTileListDataBound"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientTileListDataBound"] = value;
			}
		}

		// Token: 0x17001D1D RID: 7453
		// (get) Token: 0x06005819 RID: 22553 RVA: 0x0010D4D2 File Offset: 0x0010B6D2
		// (set) Token: 0x0600581A RID: 22554 RVA: 0x0010D4F2 File Offset: 0x0010B6F2
		[Category("Client-side events")]
		[Description("Gets or sets the name of the JavaScript function that will be called, after the client PeekTemplate is databound.")]
		[DefaultValue("")]
		[ClientControlEvent]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("clientTilePeekTemplateDataBound")]
		public string OnClientTilePeekTemplateDataBound
		{
			get
			{
				return ((string)this.ViewState["OnClientTilePeekTemplateDataBound"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientTilePeekTemplateDataBound"] = value;
			}
		}

		// Token: 0x17001D1E RID: 7454
		// (get) Token: 0x0600581B RID: 22555 RVA: 0x0010D505 File Offset: 0x0010B705
		// (set) Token: 0x0600581C RID: 22556 RVA: 0x0010D525 File Offset: 0x0010B725
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("clientTileContentTemplateDataBound")]
		[DefaultValue("")]
		[Description("Gets or sets the name of the JavaScript function that will be called, after the contentTemplate of RadContentTemplateTile is databound.")]
		[ClientControlEvent]
		public string OnClientTileContentTemplateDataBound
		{
			get
			{
				return ((string)this.ViewState["OnClientTileContentTemplateDataBound"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientTileContentTemplateDataBound"] = value;
			}
		}

		// Token: 0x17001D1F RID: 7455
		// (get) Token: 0x0600581D RID: 22557 RVA: 0x0010D538 File Offset: 0x0010B738
		// (set) Token: 0x0600581E RID: 22558 RVA: 0x0010D548 File Offset: 0x0010B748
		[SimplePersistenceSetting]
		internal List<int[]> TileGroupIndices
		{
			get
			{
				return PersistenceHelper.GetTileGroupIndicesAsList(this._tileGroupIndices);
			}
			set
			{
				RadTileListClientState radTileListClientState = new RadTileListClientState();
				radTileListClientState.TileGroupIndices = PersistenceHelper.GetTileGroupIndicesAsArrayList(value);
				if (this._initialGroupsCollection == null)
				{
					this.StoreInitialGroupCollection();
					this.SetTilesGroupIdAndIndex();
				}
				this.SetGroupsFromClientState(radTileListClientState, this._initialGroupsCollection);
			}
		}

		// Token: 0x17001D20 RID: 7456
		// (get) Token: 0x0600581F RID: 22559 RVA: 0x0010D588 File Offset: 0x0010B788
		// (set) Token: 0x06005820 RID: 22560 RVA: 0x0010D590 File Offset: 0x0010B790
		[SimplePersistenceSetting]
		internal List<string> SelectedTilesUniqueIds
		{
			get
			{
				return this.GetSelectedTilesUniqueIds();
			}
			set
			{
				this.SelectTilesByUniqueId(value);
			}
		}

		// Token: 0x06005821 RID: 22561 RVA: 0x0010D59C File Offset: 0x0010B79C
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool>(descriptor, "appendClientDataBoundItems", this.AppendClientDataBoundItems, false);
			base.DescribeProperty<bool>(descriptor, "autoPostBack", this.AutoPostBack, false);
			base.DescribeProperty<bool>(descriptor, "enableDragAndDrop", this.EnableDragAndDrop, false);
			base.DescribeProperty<string>(descriptor, "height", this.Height.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeProperty<TileListScrollingMode>(descriptor, "scrollingMode", this.ScrollingMode, TileListScrollingMode.Auto);
			base.DescribeProperty<TileListSelectionMode>(descriptor, "selectionMode", this.SelectionMode, TileListSelectionMode.None);
			base.DescribeProperty<int>(descriptor, "tileRows", this.TileRows, 0);
			base.DescribeProperty<string>(descriptor, "width", this.Width.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06005822 RID: 22562 RVA: 0x0010D66C File Offset: 0x0010B86C
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadDataBoundControl.DescribeEvent(descriptor, "load", this.OnClientLoad);
			RadDataBoundControl.DescribeEvent(descriptor, "tileClicked", this.OnClientTileClicked);
			RadDataBoundControl.DescribeEvent(descriptor, "tileClicking", this.OnClientTileClicking);
			RadDataBoundControl.DescribeEvent(descriptor, "clientTileContentTemplateDataBound", this.OnClientTileContentTemplateDataBound);
			RadDataBoundControl.DescribeEvent(descriptor, "tileCreating", this.OnClientTileCreating);
			RadDataBoundControl.DescribeEvent(descriptor, "tileDataBound", this.OnClientTileDataBound);
			RadDataBoundControl.DescribeEvent(descriptor, "tileDragging", this.OnClientTileDragging);
			RadDataBoundControl.DescribeEvent(descriptor, "tileDragStart", this.OnClientTileDragStart);
			RadDataBoundControl.DescribeEvent(descriptor, "tileDropped", this.OnClientTileDropped);
			RadDataBoundControl.DescribeEvent(descriptor, "tileDropping", this.OnClientTileDropping);
			RadDataBoundControl.DescribeEvent(descriptor, "tileListDataBound", this.OnClientTileListDataBound);
			RadDataBoundControl.DescribeEvent(descriptor, "clientTilePeekTemplateDataBound", this.OnClientTilePeekTemplateDataBound);
			RadDataBoundControl.DescribeEvent(descriptor, "tileSelected", this.OnClientTileSelected);
			RadDataBoundControl.DescribeEvent(descriptor, "tileSelecting", this.OnClientTileSelecting);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x0400156E RID: 5486
		private const string ScrollWrapperCssclass = "rtlistScrollWrapper rtlistHidden";

		// Token: 0x0400156F RID: 5487
		internal const string DataBoundTilesKey = "_!DataBoundTilesDataSource";

		// Token: 0x04001570 RID: 5488
		private TileGroupCollection _groups;

		// Token: 0x04001571 RID: 5489
		private TileListBinding _dataBindings;

		// Token: 0x04001572 RID: 5490
		private static readonly object tileClickEvent = new object();

		// Token: 0x04001573 RID: 5491
		private static readonly object tileDataBoundEvent = new object();

		// Token: 0x04001574 RID: 5492
		private static readonly object tileCreatedEvent = new object();

		// Token: 0x04001575 RID: 5493
		private static readonly object selectionChangedEvent = new object();

		// Token: 0x04001576 RID: 5494
		private List<RadBaseTile> _selectionStateChangedTiles = new List<RadBaseTile>();

		// Token: 0x04001577 RID: 5495
		private bool _dataBindingPerformed;

		// Token: 0x04001578 RID: 5496
		private ArrayList _dataBoundTilesDataSource = new ArrayList();

		// Token: 0x04001579 RID: 5497
		private TileGroupCollection _initialGroupsCollection;

		// Token: 0x0400157A RID: 5498
		private ArrayList _tileGroupIndices;
	}
}
