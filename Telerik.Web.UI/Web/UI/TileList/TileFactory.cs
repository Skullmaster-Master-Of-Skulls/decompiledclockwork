using System;
using System.Web.UI;

namespace Telerik.Web.UI.TileList
{
	// Token: 0x020008F8 RID: 2296
	internal class TileFactory
	{
		// Token: 0x17001CA9 RID: 7337
		// (get) Token: 0x060056A5 RID: 22181 RVA: 0x0010921C File Offset: 0x0010741C
		// (set) Token: 0x060056A6 RID: 22182 RVA: 0x00109224 File Offset: 0x00107424
		internal object DataObject
		{
			get
			{
				return this._dataObject;
			}
			set
			{
				this._dataObject = value;
			}
		}

		// Token: 0x17001CAA RID: 7338
		// (get) Token: 0x060056A7 RID: 22183 RVA: 0x0010922D File Offset: 0x0010742D
		// (set) Token: 0x060056A8 RID: 22184 RVA: 0x00109235 File Offset: 0x00107435
		internal TileListBinding DataBindings
		{
			get
			{
				return this._dataBindings;
			}
			set
			{
				this._dataBindings = value;
			}
		}

		// Token: 0x060056A9 RID: 22185 RVA: 0x0010923E File Offset: 0x0010743E
		public TileFactory()
		{
		}

		// Token: 0x060056AA RID: 22186 RVA: 0x00109246 File Offset: 0x00107446
		public TileFactory(TileListBinding dataBindings, object dataObject = null)
		{
			this._dataObject = dataObject;
			this._dataBindings = dataBindings;
		}

		// Token: 0x060056AB RID: 22187 RVA: 0x0010925C File Offset: 0x0010745C
		internal RadBaseTile CreateTile()
		{
			RadBaseTile radBaseTile = this.CreateEmptyTile(this.GetTileType());
			this.ApplyBindingSettings(radBaseTile);
			return radBaseTile;
		}

		// Token: 0x060056AC RID: 22188 RVA: 0x00109280 File Offset: 0x00107480
		internal RadBaseTile CreateEmptyTile(TileListTileType tileType)
		{
			RadBaseTile radBaseTile;
			try
			{
				radBaseTile = (Activator.CreateInstance(Type.GetType("Telerik.Web.UI." + tileType.ToString())) as RadBaseTile);
			}
			catch
			{
				radBaseTile = new RadTextTile();
			}
			if (this.DataBindings != null)
			{
				this.ApplyBindingTemplateSettings(radBaseTile);
				radBaseTile.IsDeclarative = false;
			}
			return radBaseTile;
		}

		// Token: 0x060056AD RID: 22189 RVA: 0x001092E4 File Offset: 0x001074E4
		private void ApplyBindingTemplateSettings(RadBaseTile tile)
		{
			bool flag = false;
			if (this.DataBindings.TilePeekTemplate != null)
			{
				tile.PeekTemplate = this.DataBindings.TilePeekTemplate;
				flag = true;
			}
			RadContentTemplateTile radContentTemplateTile = tile as RadContentTemplateTile;
			if (radContentTemplateTile != null && this.DataBindings.ContentTemplateTileBinding.ContentTemplate != null)
			{
				radContentTemplateTile.ContentTemplate = this.DataBindings.ContentTemplateTileBinding.ContentTemplate;
				flag = true;
			}
			if (flag && this.DataObject != null)
			{
				tile.DataItem = this.DataObject;
				tile.DataBind();
				tile.DataItem = null;
			}
		}

		// Token: 0x060056AE RID: 22190 RVA: 0x0010936C File Offset: 0x0010756C
		private void ApplyBindingSettings(RadBaseTile tile)
		{
			this.ApplyCommonBidning(tile);
			this.ApplyTextTileBinding(tile as RadTextTile);
			this.ApplyImageTileBinding(tile as RadImageTile);
			this.ApplyImageAndTextTileBinding(tile as RadImageAndTextTile);
			this.ApplyIconTileBinding(tile as RadIconTile);
			this.ApplyLiveTileBinding(tile as RadLiveTile);
		}

		// Token: 0x060056AF RID: 22191 RVA: 0x001093BC File Offset: 0x001075BC
		private void ApplyCommonBidning(RadBaseTile tile)
		{
			CommonTileBinding commonTileBinding = this.DataBindings.CommonTileBinding;
			tile.Shape = this.GetTileShape();
			if (!string.IsNullOrEmpty(commonTileBinding.DataNavigateUrlField))
			{
				tile.NavigateUrl = TileFactory.GetValueFromDataItem(this._dataObject, commonTileBinding.DataNavigateUrlField);
			}
			if (!string.IsNullOrEmpty(commonTileBinding.DataTargetField))
			{
				tile.Target = TileFactory.GetValueFromDataItem(this._dataObject, commonTileBinding.DataTargetField);
			}
			if (!string.IsNullOrEmpty(commonTileBinding.Target) && string.IsNullOrEmpty(tile.Target))
			{
				tile.Target = commonTileBinding.Target;
			}
			if (!string.IsNullOrEmpty(commonTileBinding.DataNameField))
			{
				tile.Name = TileFactory.GetValueFromDataItem(this._dataObject, commonTileBinding.DataNameField);
			}
			if (!string.IsNullOrEmpty(commonTileBinding.DataTitleTextField))
			{
				tile.Title.Text = TileFactory.GetValueFromDataItem(this._dataObject, commonTileBinding.DataTitleTextField);
			}
			if (!string.IsNullOrEmpty(commonTileBinding.DataTitleImageUrlField))
			{
				tile.Title.ImageUrl = TileFactory.GetValueFromDataItem(this._dataObject, commonTileBinding.DataTitleImageUrlField);
			}
			if (!string.IsNullOrEmpty(commonTileBinding.DataBadgeValueField))
			{
				string valueFromDataItem = TileFactory.GetValueFromDataItem(this._dataObject, commonTileBinding.DataBadgeValueField);
				if (!string.IsNullOrEmpty(valueFromDataItem))
				{
					tile.Badge.Value = new int?(Convert.ToInt32(valueFromDataItem));
				}
			}
			if (!string.IsNullOrEmpty(commonTileBinding.DataBadgeImageUrlField))
			{
				tile.Badge.ImageUrl = TileFactory.GetValueFromDataItem(this._dataObject, commonTileBinding.DataBadgeImageUrlField);
			}
			if (!string.IsNullOrEmpty(commonTileBinding.DataBadgePredefinedTypeField))
			{
				try
				{
					tile.Badge.PredefinedType = (TileBadgeType)Enum.Parse(typeof(TileBadgeType), TileFactory.GetValueFromDataItem(this._dataObject, commonTileBinding.DataBadgePredefinedTypeField));
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x060056B0 RID: 22192 RVA: 0x00109580 File Offset: 0x00107780
		private void ApplyLiveTileBinding(RadLiveTile tile)
		{
			if (tile != null)
			{
				if (!string.IsNullOrEmpty(this.DataBindings.LiveTileBinding.ClientTemplate))
				{
					tile.ClientTemplate = this.DataBindings.LiveTileBinding.ClientTemplate;
				}
				else if (!string.IsNullOrEmpty(this.DataBindings.LiveTileBinding.DataClientTemplateField))
				{
					tile.ClientTemplate = TileFactory.GetValueFromDataItem(this._dataObject, this.DataBindings.LiveTileBinding.DataClientTemplateField);
				}
				if (!string.IsNullOrEmpty(this.DataBindings.LiveTileBinding.DataUpdateIntervalField))
				{
					string valueFromDataItem = TileFactory.GetValueFromDataItem(this._dataObject, this.DataBindings.LiveTileBinding.DataUpdateIntervalField);
					if (!string.IsNullOrEmpty(valueFromDataItem))
					{
						tile.UpdateInterval = Convert.ToInt32(valueFromDataItem);
					}
				}
			}
		}

		// Token: 0x060056B1 RID: 22193 RVA: 0x00109641 File Offset: 0x00107841
		private void ApplyIconTileBinding(RadIconTile tile)
		{
			if (tile != null && !string.IsNullOrEmpty(this.DataBindings.IconTileBinding.DataImageUrlField))
			{
				tile.ImageUrl = TileFactory.GetValueFromDataItem(this._dataObject, this.DataBindings.IconTileBinding.DataImageUrlField);
			}
		}

		// Token: 0x060056B2 RID: 22194 RVA: 0x00109680 File Offset: 0x00107880
		private void ApplyImageAndTextTileBinding(RadImageAndTextTile tile)
		{
			if (tile != null)
			{
				if (!string.IsNullOrEmpty(this.DataBindings.ImageAndTextTileBinding.DataImageUrlField))
				{
					tile.ImageUrl = TileFactory.GetValueFromDataItem(this._dataObject, this.DataBindings.ImageAndTextTileBinding.DataImageUrlField);
				}
				if (!string.IsNullOrEmpty(this.DataBindings.ImageAndTextTileBinding.DataTextField))
				{
					tile.Text = TileFactory.GetValueFromDataItem(this._dataObject, this.DataBindings.ImageAndTextTileBinding.DataTextField);
				}
			}
		}

		// Token: 0x060056B3 RID: 22195 RVA: 0x00109700 File Offset: 0x00107900
		private void ApplyImageTileBinding(RadImageTile tile)
		{
			if (tile != null && !string.IsNullOrEmpty(this.DataBindings.ImageTileBinding.DataImageUrlField))
			{
				tile.ImageUrl = TileFactory.GetValueFromDataItem(this._dataObject, this.DataBindings.ImageTileBinding.DataImageUrlField);
			}
		}

		// Token: 0x060056B4 RID: 22196 RVA: 0x0010973D File Offset: 0x0010793D
		private void ApplyTextTileBinding(RadTextTile tile)
		{
			if (tile != null && !string.IsNullOrEmpty(this.DataBindings.TextTileBinding.DataTextField))
			{
				tile.Text = TileFactory.GetValueFromDataItem(this._dataObject, this.DataBindings.TextTileBinding.DataTextField);
			}
		}

		// Token: 0x060056B5 RID: 22197 RVA: 0x0010977C File Offset: 0x0010797C
		private TileShape GetTileShape()
		{
			TileShape result = TileShape.Square;
			CommonTileBinding commonTileBinding = this.DataBindings.CommonTileBinding;
			if (commonTileBinding != null)
			{
				try
				{
					result = (TileShape)Enum.Parse(typeof(TileShape), TileFactory.GetValueFromDataItem(this._dataObject, commonTileBinding.DataShapeField));
				}
				catch (Exception)
				{
					result = commonTileBinding.Shape;
				}
			}
			return result;
		}

		// Token: 0x060056B6 RID: 22198 RVA: 0x001097E0 File Offset: 0x001079E0
		private TileListTileType GetTileType()
		{
			TileListTileType result = TileListTileType.RadTextTile;
			CommonTileBinding commonTileBinding = this.DataBindings.CommonTileBinding;
			if (commonTileBinding != null)
			{
				try
				{
					result = (TileListTileType)Enum.Parse(typeof(TileListTileType), TileFactory.GetValueFromDataItem(this._dataObject, commonTileBinding.DataTileTypeField));
				}
				catch (Exception)
				{
					result = commonTileBinding.TileType;
				}
			}
			return result;
		}

		// Token: 0x060056B7 RID: 22199 RVA: 0x00109844 File Offset: 0x00107A44
		internal static string GetValueFromDataItem(object dataItem, string dataValueField)
		{
			string result = null;
			if (!string.IsNullOrEmpty(dataValueField))
			{
				result = DataBinder.GetPropertyValue(dataItem, dataValueField, null);
			}
			return result;
		}

		// Token: 0x04001527 RID: 5415
		private object _dataObject;

		// Token: 0x04001528 RID: 5416
		private TileListBinding _dataBindings;
	}
}
