using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x020019C3 RID: 6595
	internal class RadListViewJavaScriptConverter : JavaScriptConverter
	{
		// Token: 0x0600FEAD RID: 65197 RVA: 0x00392AC5 File Offset: 0x00390CC5
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600FEAE RID: 65198 RVA: 0x00392ACC File Offset: 0x00390CCC
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			RadListViewClientSettings radListViewClientSettings = obj as RadListViewClientSettings;
			if (radListViewClientSettings != null)
			{
				if (radListViewClientSettings.ShouldSerializePostBackFunction)
				{
					dictionary.Add("PostBackFunction", radListViewClientSettings.PostBackFunction);
				}
				if (radListViewClientSettings.ShouldSerializeAllowItemsDragDrop)
				{
					dictionary.Add("AllowItemsDragDrop", radListViewClientSettings.AllowItemsDragDrop);
				}
				dictionary.Add("DataBinding", radListViewClientSettings.DataBinding);
			}
			RadListViewClientDataBinding radListViewClientDataBinding = obj as RadListViewClientDataBinding;
			if (radListViewClientDataBinding != null)
			{
				if (!string.IsNullOrEmpty(radListViewClientDataBinding.ContainerID))
				{
					dictionary.Add("ContainerID", radListViewClientDataBinding.ContainerID);
				}
				if (!string.IsNullOrEmpty(radListViewClientDataBinding.ItemPlaceHolderID))
				{
					dictionary.Add("ItemPlaceHolderID", radListViewClientDataBinding.ItemPlaceHolderID);
				}
				if (!string.IsNullOrEmpty(radListViewClientDataBinding.LayoutTemplate))
				{
					dictionary.Add("LayoutTemplate", radListViewClientDataBinding.LayoutTemplate);
				}
				if (!string.IsNullOrEmpty(radListViewClientDataBinding.ItemTemplate))
				{
					dictionary.Add("ItemTemplate", radListViewClientDataBinding.ItemTemplate);
				}
				if (!string.IsNullOrEmpty(radListViewClientDataBinding.AlternatingItemTemplate))
				{
					dictionary.Add("AlternatingItemTemplate", radListViewClientDataBinding.AlternatingItemTemplate);
				}
				if (!string.IsNullOrEmpty(radListViewClientDataBinding.ItemSeparatorTemplate))
				{
					dictionary.Add("ItemSeparatorTemplate", radListViewClientDataBinding.ItemSeparatorTemplate);
				}
				if (!string.IsNullOrEmpty(radListViewClientDataBinding.EmptyDataTemplate))
				{
					dictionary.Add("EmptyDataTemplate", radListViewClientDataBinding.EmptyDataTemplate);
				}
				if (!string.IsNullOrEmpty(radListViewClientDataBinding.SelectedItemTemplate))
				{
					dictionary.Add("SelectedItemTemplate", radListViewClientDataBinding.SelectedItemTemplate);
				}
				dictionary.Add("DataService", radListViewClientDataBinding.DataService);
			}
			RadListViewDataServiceSettings radListViewDataServiceSettings = obj as RadListViewDataServiceSettings;
			if (radListViewDataServiceSettings != null)
			{
				if (!string.IsNullOrEmpty(radListViewDataServiceSettings.Location))
				{
					dictionary.Add("Location", HttpContext.Current.Response.ApplyAppPathModifier(radListViewDataServiceSettings.Location));
				}
				if (radListViewDataServiceSettings.HttpMethod != RadListViewDataServiceHttpMethod.Post)
				{
					dictionary.Add("HttpMethod", radListViewDataServiceSettings.HttpMethod.ToString());
				}
				if (!string.IsNullOrEmpty(radListViewDataServiceSettings.DataPath))
				{
					dictionary.Add("DataPath", radListViewDataServiceSettings.DataPath);
				}
				if (!string.IsNullOrEmpty(radListViewDataServiceSettings.CountPath))
				{
					dictionary.Add("CountPath", radListViewDataServiceSettings.CountPath);
				}
				if (radListViewDataServiceSettings.EnableCaching)
				{
					dictionary.Add("EnableCaching", radListViewDataServiceSettings.EnableCaching);
				}
				if (!string.IsNullOrEmpty(radListViewDataServiceSettings.DataPropertyName))
				{
					dictionary.Add("DataPropertyName", radListViewDataServiceSettings.DataPropertyName);
				}
				if (!string.IsNullOrEmpty(radListViewDataServiceSettings.CountPropertyName))
				{
					dictionary.Add("CountPropertyName", radListViewDataServiceSettings.CountPropertyName);
				}
				if (!string.IsNullOrEmpty(radListViewDataServiceSettings.FilterParameterName))
				{
					dictionary.Add("FilterParameterName", radListViewDataServiceSettings.FilterParameterName);
				}
				if (radListViewDataServiceSettings.FilterParameterType != RadListViewClientDataBindingParameterType.List)
				{
					dictionary.Add("FilterParameterType", radListViewDataServiceSettings.FilterParameterType.ToString());
				}
				if (!string.IsNullOrEmpty(radListViewDataServiceSettings.SortParameterName))
				{
					dictionary.Add("SortParameterName", radListViewDataServiceSettings.SortParameterName);
				}
				if (radListViewDataServiceSettings.SortParameterType != RadListViewClientDataBindingParameterType.List)
				{
					dictionary.Add("SortParameterType", radListViewDataServiceSettings.SortParameterType.ToString());
				}
				if (!string.IsNullOrEmpty(radListViewDataServiceSettings.StartRowIndexParameterName))
				{
					dictionary.Add("StartRowIndexParameterName", radListViewDataServiceSettings.StartRowIndexParameterName);
				}
				if (!string.IsNullOrEmpty(radListViewDataServiceSettings.MaximumRowsParameterName))
				{
					dictionary.Add("MaximumRowsParameterName", radListViewDataServiceSettings.MaximumRowsParameterName);
				}
				if (radListViewDataServiceSettings.ResponseType != RadListViewDataServiceResponseType.JSON)
				{
					dictionary.Add("ResponseType", radListViewDataServiceSettings.ResponseType.ToString());
				}
			}
			return dictionary;
		}

		// Token: 0x17004CDF RID: 19679
		// (get) Token: 0x0600FEAF RID: 65199 RVA: 0x00392F28 File Offset: 0x00391128
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(RadListViewClientSettings);
				yield return typeof(RadListViewClientDataBinding);
				yield return typeof(RadListViewDataServiceSettings);
				yield break;
			}
		}
	}
}
