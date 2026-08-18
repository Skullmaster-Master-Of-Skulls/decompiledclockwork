using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x020001DA RID: 474
	public class ViewDataDictionary : IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable
	{
		// Token: 0x06000E15 RID: 3605 RVA: 0x00025510 File Offset: 0x00023710
		public ViewDataDictionary() : this(null)
		{
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x00025519 File Offset: 0x00023719
		public ViewDataDictionary(object model)
		{
			this.Model = model;
			this._innerDictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
			this._modelState = new ModelStateDictionary();
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x00025544 File Offset: 0x00023744
		public ViewDataDictionary(ViewDataDictionary dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this._innerDictionary = new CopyOnWriteDictionary<string, object>(dictionary, StringComparer.OrdinalIgnoreCase);
			this._modelState = new ModelStateDictionary(dictionary.ModelState);
			this.Model = dictionary.Model;
			this.TemplateInfo = dictionary.TemplateInfo;
			this._modelMetadata = dictionary._modelMetadata;
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000E18 RID: 3608 RVA: 0x000255AB File Offset: 0x000237AB
		public int Count
		{
			get
			{
				return this._innerDictionary.Count;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000E19 RID: 3609 RVA: 0x000255B8 File Offset: 0x000237B8
		public bool IsReadOnly
		{
			get
			{
				return this._innerDictionary.IsReadOnly;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000E1A RID: 3610 RVA: 0x000255C5 File Offset: 0x000237C5
		public ICollection<string> Keys
		{
			get
			{
				return this._innerDictionary.Keys;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000E1B RID: 3611 RVA: 0x000255D2 File Offset: 0x000237D2
		// (set) Token: 0x06000E1C RID: 3612 RVA: 0x000255DA File Offset: 0x000237DA
		public object Model
		{
			get
			{
				return this._model;
			}
			set
			{
				this._modelMetadata = null;
				this.SetModel(value);
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000E1D RID: 3613 RVA: 0x000255F4 File Offset: 0x000237F4
		// (set) Token: 0x06000E1E RID: 3614 RVA: 0x00025645 File Offset: 0x00023845
		public virtual ModelMetadata ModelMetadata
		{
			get
			{
				if (this._modelMetadata == null && this._model != null)
				{
					this._modelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => this._model, this._model.GetType());
				}
				return this._modelMetadata;
			}
			set
			{
				this._modelMetadata = value;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x0002564E File Offset: 0x0002384E
		public ModelStateDictionary ModelState
		{
			get
			{
				return this._modelState;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000E20 RID: 3616 RVA: 0x00025656 File Offset: 0x00023856
		// (set) Token: 0x06000E21 RID: 3617 RVA: 0x00025671 File Offset: 0x00023871
		public TemplateInfo TemplateInfo
		{
			get
			{
				if (this._templateMetadata == null)
				{
					this._templateMetadata = new TemplateInfo();
				}
				return this._templateMetadata;
			}
			set
			{
				this._templateMetadata = value;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000E22 RID: 3618 RVA: 0x0002567A File Offset: 0x0002387A
		public ICollection<object> Values
		{
			get
			{
				return this._innerDictionary.Values;
			}
		}

		// Token: 0x17000322 RID: 802
		public object this[string key]
		{
			get
			{
				object result;
				this._innerDictionary.TryGetValue(key, out result);
				return result;
			}
			set
			{
				this._innerDictionary[key] = value;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000E25 RID: 3621 RVA: 0x000256B4 File Offset: 0x000238B4
		internal IDictionary<string, object> InnerDictionary
		{
			get
			{
				return this._innerDictionary;
			}
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x000256BC File Offset: 0x000238BC
		public void Add(KeyValuePair<string, object> item)
		{
			this._innerDictionary.Add(item);
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x000256CA File Offset: 0x000238CA
		public void Add(string key, object value)
		{
			this._innerDictionary.Add(key, value);
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x000256D9 File Offset: 0x000238D9
		public void Clear()
		{
			this._innerDictionary.Clear();
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x000256E6 File Offset: 0x000238E6
		public bool Contains(KeyValuePair<string, object> item)
		{
			return this._innerDictionary.Contains(item);
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x000256F4 File Offset: 0x000238F4
		public bool ContainsKey(string key)
		{
			return this._innerDictionary.ContainsKey(key);
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x00025702 File Offset: 0x00023902
		public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
		{
			this._innerDictionary.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x00025714 File Offset: 0x00023914
		public object Eval(string expression)
		{
			ViewDataInfo viewDataInfo = this.GetViewDataInfo(expression);
			if (viewDataInfo == null)
			{
				return null;
			}
			return viewDataInfo.Value;
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x00025734 File Offset: 0x00023934
		public string Eval(string expression, string format)
		{
			object value = this.Eval(expression);
			return ViewDataDictionary.FormatValueInternal(value, format);
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x00025750 File Offset: 0x00023950
		internal static string FormatValueInternal(object value, string format)
		{
			if (value == null)
			{
				return string.Empty;
			}
			if (string.IsNullOrEmpty(format))
			{
				return Convert.ToString(value, CultureInfo.CurrentCulture);
			}
			return string.Format(CultureInfo.CurrentCulture, format, new object[]
			{
				value
			});
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x00025791 File Offset: 0x00023991
		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			return this._innerDictionary.GetEnumerator();
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x0002579E File Offset: 0x0002399E
		public ViewDataInfo GetViewDataInfo(string expression)
		{
			if (string.IsNullOrEmpty(expression))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "expression");
			}
			return ViewDataDictionary.ViewDataEvaluator.Eval(this, expression);
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x000257BF File Offset: 0x000239BF
		public bool Remove(KeyValuePair<string, object> item)
		{
			return this._innerDictionary.Remove(item);
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x000257CD File Offset: 0x000239CD
		public bool Remove(string key)
		{
			return this._innerDictionary.Remove(key);
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x000257DB File Offset: 0x000239DB
		protected virtual void SetModel(object value)
		{
			this._model = value;
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x000257E4 File Offset: 0x000239E4
		public bool TryGetValue(string key, out object value)
		{
			return this._innerDictionary.TryGetValue(key, out value);
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x000257F3 File Offset: 0x000239F3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._innerDictionary.GetEnumerator();
		}

		// Token: 0x040003B9 RID: 953
		private readonly IDictionary<string, object> _innerDictionary;

		// Token: 0x040003BA RID: 954
		private readonly ModelStateDictionary _modelState;

		// Token: 0x040003BB RID: 955
		private object _model;

		// Token: 0x040003BC RID: 956
		private ModelMetadata _modelMetadata;

		// Token: 0x040003BD RID: 957
		private TemplateInfo _templateMetadata;

		// Token: 0x020001DB RID: 475
		internal static class ViewDataEvaluator
		{
			// Token: 0x06000E37 RID: 3639 RVA: 0x00025800 File Offset: 0x00023A00
			public static ViewDataInfo Eval(ViewDataDictionary vdd, string expression)
			{
				return ViewDataDictionary.ViewDataEvaluator.EvalComplexExpression(vdd, expression);
			}

			// Token: 0x06000E38 RID: 3640 RVA: 0x00025818 File Offset: 0x00023A18
			private static ViewDataInfo EvalComplexExpression(object indexableObject, string expression)
			{
				foreach (ViewDataDictionary.ViewDataEvaluator.ExpressionPair expressionPair in ViewDataDictionary.ViewDataEvaluator.GetRightToLeftExpressions(expression))
				{
					string left = expressionPair.Left;
					string right = expressionPair.Right;
					ViewDataInfo propertyValue = ViewDataDictionary.ViewDataEvaluator.GetPropertyValue(indexableObject, left);
					if (propertyValue != null)
					{
						if (string.IsNullOrEmpty(right))
						{
							return propertyValue;
						}
						if (propertyValue.Value != null)
						{
							ViewDataInfo viewDataInfo = ViewDataDictionary.ViewDataEvaluator.EvalComplexExpression(propertyValue.Value, right);
							if (viewDataInfo != null)
							{
								return viewDataInfo;
							}
						}
					}
				}
				return null;
			}

			// Token: 0x06000E39 RID: 3641 RVA: 0x00025A30 File Offset: 0x00023C30
			private static IEnumerable<ViewDataDictionary.ViewDataEvaluator.ExpressionPair> GetRightToLeftExpressions(string expression)
			{
				yield return new ViewDataDictionary.ViewDataEvaluator.ExpressionPair(expression, string.Empty);
				int lastDot = expression.LastIndexOf('.');
				string subExpression = expression;
				string postExpression = string.Empty;
				while (lastDot > -1)
				{
					subExpression = expression.Substring(0, lastDot);
					postExpression = expression.Substring(lastDot + 1);
					yield return new ViewDataDictionary.ViewDataEvaluator.ExpressionPair(subExpression, postExpression);
					lastDot = subExpression.LastIndexOf('.');
				}
				yield break;
			}

			// Token: 0x06000E3A RID: 3642 RVA: 0x00025A50 File Offset: 0x00023C50
			private static ViewDataInfo GetIndexedPropertyValue(object indexableObject, string key)
			{
				IDictionary<string, object> dictionary = indexableObject as IDictionary<string, object>;
				object value = null;
				bool flag = false;
				if (dictionary != null)
				{
					flag = dictionary.TryGetValue(key, out value);
				}
				else
				{
					TryGetValueDelegate tryGetValueDelegate = TypeHelpers.CreateTryGetValueDelegate(indexableObject.GetType());
					if (tryGetValueDelegate != null)
					{
						flag = tryGetValueDelegate(indexableObject, key, out value);
					}
				}
				if (flag)
				{
					return new ViewDataInfo
					{
						Container = indexableObject,
						Value = value
					};
				}
				return null;
			}

			// Token: 0x06000E3B RID: 3643 RVA: 0x00025ACC File Offset: 0x00023CCC
			private static ViewDataInfo GetPropertyValue(object container, string propertyName)
			{
				ViewDataInfo indexedPropertyValue = ViewDataDictionary.ViewDataEvaluator.GetIndexedPropertyValue(container, propertyName);
				if (indexedPropertyValue != null)
				{
					return indexedPropertyValue;
				}
				ViewDataDictionary viewDataDictionary = container as ViewDataDictionary;
				if (viewDataDictionary != null)
				{
					container = viewDataDictionary.Model;
				}
				if (container == null)
				{
					return null;
				}
				PropertyDescriptor descriptor = TypeDescriptor.GetProperties(container).Find(propertyName, true);
				if (descriptor == null)
				{
					return null;
				}
				return new ViewDataInfo(() => descriptor.GetValue(container))
				{
					Container = container,
					PropertyDescriptor = descriptor
				};
			}

			// Token: 0x020001DC RID: 476
			private struct ExpressionPair
			{
				// Token: 0x06000E3C RID: 3644 RVA: 0x00025B6A File Offset: 0x00023D6A
				public ExpressionPair(string left, string right)
				{
					this.Left = left;
					this.Right = right;
				}

				// Token: 0x040003BE RID: 958
				public readonly string Left;

				// Token: 0x040003BF RID: 959
				public readonly string Right;
			}
		}
	}
}
