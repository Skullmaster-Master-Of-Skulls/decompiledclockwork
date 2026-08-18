using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Compilation;
using System.Web.ModelBinding;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200047B RID: 1147
	public class ModelDataSourceView : DataSourceView, IStateManager
	{
		// Token: 0x060038B0 RID: 14512 RVA: 0x000B8418 File Offset: 0x000B6618
		public ModelDataSourceView(ModelDataSource owner) : base(owner, "DefaultView")
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			this._owner = owner;
			if (owner.DataControl.Page != null)
			{
				owner.DataControl.Page.LoadComplete += this.OnPageLoadComplete;
			}
		}

		// Token: 0x17001095 RID: 4245
		// (get) Token: 0x060038B1 RID: 14513 RVA: 0x000B846F File Offset: 0x000B666F
		public override bool CanDelete
		{
			get
			{
				return this.DeleteMethod.Length != 0;
			}
		}

		// Token: 0x17001096 RID: 4246
		// (get) Token: 0x060038B2 RID: 14514 RVA: 0x000B847F File Offset: 0x000B667F
		public override bool CanInsert
		{
			get
			{
				return this.InsertMethod.Length != 0;
			}
		}

		// Token: 0x17001097 RID: 4247
		// (get) Token: 0x060038B3 RID: 14515 RVA: 0x000097B7 File Offset: 0x000079B7
		public override bool CanPage
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001098 RID: 4248
		// (get) Token: 0x060038B4 RID: 14516 RVA: 0x000097B7 File Offset: 0x000079B7
		public override bool CanSort
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001099 RID: 4249
		// (get) Token: 0x060038B5 RID: 14517 RVA: 0x000097B7 File Offset: 0x000079B7
		public override bool CanRetrieveTotalRowCount
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700109A RID: 4250
		// (get) Token: 0x060038B6 RID: 14518 RVA: 0x000B848F File Offset: 0x000B668F
		public override bool CanUpdate
		{
			get
			{
				return this.UpdateMethod.Length != 0;
			}
		}

		// Token: 0x1700109B RID: 4251
		// (get) Token: 0x060038B7 RID: 14519 RVA: 0x000B849F File Offset: 0x000B669F
		// (set) Token: 0x060038B8 RID: 14520 RVA: 0x000B84B0 File Offset: 0x000B66B0
		public string ModelTypeName
		{
			get
			{
				return this._modelTypeName ?? string.Empty;
			}
			internal set
			{
				if (this._modelTypeName != value)
				{
					this._modelTypeName = value;
					this.OnDataSourceViewChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x1700109C RID: 4252
		// (get) Token: 0x060038B9 RID: 14521 RVA: 0x000B84D2 File Offset: 0x000B66D2
		// (set) Token: 0x060038BA RID: 14522 RVA: 0x000B84E3 File Offset: 0x000B66E3
		public string DeleteMethod
		{
			get
			{
				return this._deleteMethod ?? string.Empty;
			}
			internal set
			{
				this._deleteMethod = value;
			}
		}

		// Token: 0x1700109D RID: 4253
		// (get) Token: 0x060038BB RID: 14523 RVA: 0x000B84EC File Offset: 0x000B66EC
		// (set) Token: 0x060038BC RID: 14524 RVA: 0x000B84FD File Offset: 0x000B66FD
		public string InsertMethod
		{
			get
			{
				return this._insertMethod ?? string.Empty;
			}
			internal set
			{
				this._insertMethod = value;
			}
		}

		// Token: 0x1700109E RID: 4254
		// (get) Token: 0x060038BD RID: 14525 RVA: 0x000B8506 File Offset: 0x000B6706
		// (set) Token: 0x060038BE RID: 14526 RVA: 0x000B8517 File Offset: 0x000B6717
		public string SelectMethod
		{
			get
			{
				return this._selectMethod ?? string.Empty;
			}
			internal set
			{
				if (this._selectMethod != value)
				{
					this._selectMethod = value;
					this.OnDataSourceViewChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x1700109F RID: 4255
		// (get) Token: 0x060038BF RID: 14527 RVA: 0x000B8539 File Offset: 0x000B6739
		// (set) Token: 0x060038C0 RID: 14528 RVA: 0x000B854A File Offset: 0x000B674A
		public string UpdateMethod
		{
			get
			{
				return this._updateMethod ?? string.Empty;
			}
			internal set
			{
				this._updateMethod = value;
			}
		}

		// Token: 0x170010A0 RID: 4256
		// (get) Token: 0x060038C1 RID: 14529 RVA: 0x000B8553 File Offset: 0x000B6753
		// (set) Token: 0x060038C2 RID: 14530 RVA: 0x000B8564 File Offset: 0x000B6764
		public string DataKeyName
		{
			get
			{
				return this._dataKeyName ?? string.Empty;
			}
			internal set
			{
				if (this._dataKeyName != value)
				{
					this._dataKeyName = value;
					this.OnDataSourceViewChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x140000BF RID: 191
		// (add) Token: 0x060038C3 RID: 14531 RVA: 0x000B8586 File Offset: 0x000B6786
		// (remove) Token: 0x060038C4 RID: 14532 RVA: 0x000B8599 File Offset: 0x000B6799
		public event CallingDataMethodsEventHandler CallingDataMethods
		{
			add
			{
				base.Events.AddHandler(ModelDataSourceView.EventCallingDataMethods, value);
			}
			remove
			{
				base.Events.RemoveHandler(ModelDataSourceView.EventCallingDataMethods, value);
			}
		}

		// Token: 0x060038C5 RID: 14533 RVA: 0x000B85AC File Offset: 0x000B67AC
		public void UpdateProperties(string modelTypeName, string selectMethod, string updateMethod, string insertMethod, string deleteMethod, string dataKeyName)
		{
			this.ModelTypeName = modelTypeName;
			this.SelectMethod = selectMethod;
			this.UpdateMethod = updateMethod;
			this.InsertMethod = insertMethod;
			this.DeleteMethod = deleteMethod;
			this.DataKeyName = dataKeyName;
		}

		// Token: 0x060038C6 RID: 14534 RVA: 0x000B85DC File Offset: 0x000B67DC
		protected virtual void OnCallingDataMethods(CallingDataMethodsEventArgs e)
		{
			CallingDataMethodsEventHandler callingDataMethodsEventHandler = base.Events[ModelDataSourceView.EventCallingDataMethods] as CallingDataMethodsEventHandler;
			if (callingDataMethodsEventHandler != null)
			{
				callingDataMethodsEventHandler(this._owner.DataControl, e);
			}
		}

		// Token: 0x060038C7 RID: 14535 RVA: 0x000B8614 File Offset: 0x000B6814
		private void OnPageLoadComplete(object sender, EventArgs e)
		{
			this.EvaluateSelectParameters();
		}

		// Token: 0x060038C8 RID: 14536 RVA: 0x000B861C File Offset: 0x000B681C
		private static bool IsAutoPagingRequired(MethodInfo selectMethod, bool isReturningQueryable, bool isAsyncSelect)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			foreach (ParameterInfo parameterInfo in selectMethod.GetParameters())
			{
				string name = parameterInfo.Name;
				if (string.Equals("startRowIndex", name, StringComparison.OrdinalIgnoreCase))
				{
					if (parameterInfo.ParameterType.IsAssignableFrom(typeof(int)))
					{
						flag3 = true;
					}
				}
				else if (string.Equals("maximumRows", name, StringComparison.OrdinalIgnoreCase))
				{
					if (parameterInfo.ParameterType.IsAssignableFrom(typeof(int)))
					{
						flag = true;
					}
				}
				else if (string.Equals("totalRowCount", name, StringComparison.OrdinalIgnoreCase) && parameterInfo.IsOut && typeof(int).IsAssignableFrom(parameterInfo.ParameterType.GetElementType()))
				{
					flag2 = true;
				}
			}
			bool flag4;
			if (isAsyncSelect)
			{
				flag4 = (flag && flag3);
			}
			else
			{
				flag4 = (flag && flag3 && flag2);
			}
			if (isReturningQueryable || flag4)
			{
				return !flag4;
			}
			if (isAsyncSelect)
			{
				throw new InvalidOperationException(SR.GetString("ModelDataSourceView_InvalidAsyncPagingParameters"));
			}
			throw new InvalidOperationException(SR.GetString("ModelDataSourceView_InvalidPagingParameters"));
		}

		// Token: 0x060038C9 RID: 14537 RVA: 0x000B872C File Offset: 0x000B692C
		private static bool IsAutoSortingRequired(MethodInfo selectMethod, bool isReturningQueryable)
		{
			bool flag = false;
			foreach (ParameterInfo parameterInfo in selectMethod.GetParameters())
			{
				string name = parameterInfo.Name;
				if (string.Equals("sortByExpression", name, StringComparison.OrdinalIgnoreCase) && parameterInfo.ParameterType.IsAssignableFrom(typeof(string)))
				{
					flag = true;
				}
			}
			if (!isReturningQueryable && !flag)
			{
				throw new InvalidOperationException(SR.GetString("ModelDataSourceView_InvalidSortingParameters"));
			}
			return !flag;
		}

		// Token: 0x060038CA RID: 14538 RVA: 0x000B87A0 File Offset: 0x000B69A0
		private object GetPropertyValueByName(object o, string name)
		{
			PropertyInfo property = o.GetType().GetProperty(name);
			return property.GetValue(o, null);
		}

		// Token: 0x060038CB RID: 14539 RVA: 0x000B87C4 File Offset: 0x000B69C4
		private void ValidateAsyncModelBindingRequirements()
		{
			if (!this._owner.DataControl.Page.IsAsync || SynchronizationContextUtil.CurrentMode == SynchronizationContextMode.Legacy)
			{
				throw new InvalidOperationException(SR.GetString("ModelDataSourceView_UseAsyncMethodMustBeUsingAsyncPage"));
			}
		}

		// Token: 0x060038CC RID: 14540 RVA: 0x000B87F8 File Offset: 0x000B69F8
		private bool RequireAsyncModelBinding(string methodName, out ModelDataSourceMethod method)
		{
			if (!AppSettings.EnableAsyncModelBinding)
			{
				method = null;
				return false;
			}
			method = this.FindMethod(methodName);
			if (method == null)
			{
				return false;
			}
			MethodInfo methodInfo = method.MethodInfo;
			return typeof(Task).IsAssignableFrom(methodInfo.ReturnType);
		}

		// Token: 0x060038CD RID: 14541 RVA: 0x000B8840 File Offset: 0x000B6A40
		protected virtual object GetSelectMethodResult(DataSourceSelectArguments arguments)
		{
			if (this.SelectMethod.Length == 0)
			{
				throw new InvalidOperationException(SR.GetString("ModelDataSourceView_SelectNotSupported"));
			}
			DataSourceSelectResultProcessingOptions selectResultProcessingOptions = null;
			ModelDataSourceMethod method = this.EvaluateSelectMethodParameters(arguments, out selectResultProcessingOptions);
			ModelDataMethodResult result = this.InvokeMethod(method);
			return this.ProcessSelectMethodResult(arguments, selectResultProcessingOptions, result);
		}

		// Token: 0x060038CE RID: 14542 RVA: 0x000B8887 File Offset: 0x000B6A87
		protected virtual ModelDataSourceMethod EvaluateSelectMethodParameters(DataSourceSelectArguments arguments, out DataSourceSelectResultProcessingOptions selectResultProcessingOptions)
		{
			return this.EvaluateSelectMethodParameters(arguments, null, false, out selectResultProcessingOptions);
		}

		// Token: 0x060038CF RID: 14543 RVA: 0x000B8894 File Offset: 0x000B6A94
		private ModelDataSourceMethod EvaluateSelectMethodParameters(DataSourceSelectArguments arguments, ModelDataSourceMethod method, bool isAsyncSelect, out DataSourceSelectResultProcessingOptions selectResultProcessingOptions)
		{
			IOrderedDictionary controlValues = ModelDataSourceView.MergeSelectParameters(arguments);
			method = (method ?? this.FindMethod(this.SelectMethod));
			Type type = method.MethodInfo.ReturnType;
			if (isAsyncSelect)
			{
				type = this.ExtractAsyncSelectReturnType(type);
			}
			Type type2 = this.ModelType;
			if (type2 == null)
			{
				foreach (Type type3 in type.GetGenericArguments())
				{
					if (typeof(IQueryable<>).MakeGenericType(new Type[]
					{
						type3
					}).IsAssignableFrom(type))
					{
						type2 = type3;
					}
				}
			}
			Type type4 = (type2 != null) ? typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				type2
			}) : null;
			bool flag = type4 != null && type4.IsAssignableFrom(type);
			if (isAsyncSelect && flag)
			{
				throw new InvalidOperationException(SR.GetString("ModelDataSourceView_InvalidAsyncSelectReturnType", new object[]
				{
					type2
				}));
			}
			bool autoPage = false;
			bool autoSort = false;
			if (arguments.StartRowIndex >= 0 && arguments.MaximumRows > 0)
			{
				autoPage = ModelDataSourceView.IsAutoPagingRequired(method.MethodInfo, flag, isAsyncSelect);
				if (isAsyncSelect && typeof(SelectResult) != type)
				{
					throw new InvalidOperationException(SR.GetString("ModelDataSourceView_MustUseSelectResultAsReturnType"));
				}
			}
			if (!string.IsNullOrEmpty(arguments.SortExpression))
			{
				autoSort = ModelDataSourceView.IsAutoSortingRequired(method.MethodInfo, flag);
			}
			selectResultProcessingOptions = new DataSourceSelectResultProcessingOptions
			{
				ModelType = type2,
				AutoPage = autoPage,
				AutoSort = autoSort
			};
			this.EvaluateMethodParameters(DataSourceOperation.Select, method, controlValues);
			return method;
		}

		// Token: 0x060038D0 RID: 14544 RVA: 0x000B8A18 File Offset: 0x000B6C18
		private Type ExtractAsyncSelectReturnType(Type t)
		{
			if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Task<>))
			{
				Type[] genericArguments = t.GetGenericArguments();
				if (genericArguments.Length == 1)
				{
					return genericArguments[0];
				}
			}
			throw new InvalidOperationException(SR.GetString("ModelDataSourceView_InvalidAsyncSelectReturnType", new object[]
			{
				this.ModelType
			}));
		}

		// Token: 0x060038D1 RID: 14545 RVA: 0x000B8A74 File Offset: 0x000B6C74
		protected virtual object ProcessSelectMethodResult(DataSourceSelectArguments arguments, DataSourceSelectResultProcessingOptions selectResultProcessingOptions, ModelDataMethodResult result)
		{
			if (result.ReturnValue == null)
			{
				return null;
			}
			bool autoPage = selectResultProcessingOptions.AutoPage;
			bool flag = selectResultProcessingOptions.AutoSort;
			Type modelType = selectResultProcessingOptions.ModelType;
			string text = arguments.SortExpression;
			if (autoPage)
			{
				MethodInfo methodInfo = typeof(QueryableHelpers).GetMethod("CountHelper").MakeGenericMethod(new Type[]
				{
					modelType
				});
				arguments.TotalRowCount = (int)methodInfo.Invoke(null, new object[]
				{
					result.ReturnValue
				});
				MethodInfo methodInfo2 = typeof(QueryableHelpers).GetMethod("IsOrderingMethodFound").MakeGenericMethod(new Type[]
				{
					modelType
				});
				if (!(bool)methodInfo2.Invoke(null, new object[]
				{
					result.ReturnValue
				}) && string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(this.DataKeyName))
				{
					flag = true;
					selectResultProcessingOptions.AutoSort = true;
					text = this.DataKeyName;
				}
			}
			else if (arguments.StartRowIndex >= 0 && arguments.MaximumRows > 0)
			{
				arguments.TotalRowCount = (int)result.OutputParameters["totalRowCount"];
			}
			if (autoPage || flag)
			{
				MethodInfo methodInfo3 = typeof(QueryableHelpers).GetMethod("SortandPageHelper").MakeGenericMethod(new Type[]
				{
					modelType
				});
				return methodInfo3.Invoke(null, new object[]
				{
					result.ReturnValue,
					autoPage ? new int?(arguments.StartRowIndex) : null,
					autoPage ? new int?(arguments.MaximumRows) : null,
					flag ? text : null
				});
			}
			return result.ReturnValue;
		}

		// Token: 0x060038D2 RID: 14546 RVA: 0x000B8C2C File Offset: 0x000B6E2C
		private static IOrderedDictionary MergeSelectParameters(DataSourceSelectArguments arguments)
		{
			bool flag = arguments.StartRowIndex >= 0 && arguments.MaximumRows > 0;
			bool flag2 = !string.IsNullOrEmpty(arguments.SortExpression);
			IOrderedDictionary orderedDictionary = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
			if (flag2)
			{
				orderedDictionary["sortByExpression"] = arguments.SortExpression;
			}
			if (flag)
			{
				IDictionary dictionary = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
				dictionary["maximumRows"] = arguments.MaximumRows;
				dictionary["startRowIndex"] = arguments.StartRowIndex;
				dictionary["totalRowCount"] = 0;
				ModelDataSourceView.MergeDictionaries(dictionary, orderedDictionary);
			}
			return orderedDictionary;
		}

		// Token: 0x060038D3 RID: 14547 RVA: 0x000B8CD0 File Offset: 0x000B6ED0
		protected virtual IEnumerable CreateSelectResult(object result)
		{
			return this.CreateSelectResult(result, false);
		}

		// Token: 0x060038D4 RID: 14548 RVA: 0x000B8CDC File Offset: 0x000B6EDC
		private IEnumerable CreateSelectResult(object result, bool isAsyncSelect)
		{
			if (result == null)
			{
				return null;
			}
			Type modelType = this.ModelType;
			Type type = (modelType != null) ? typeof(IEnumerable<>).MakeGenericType(new Type[]
			{
				modelType
			}) : typeof(IEnumerable);
			if (type.IsInstanceOfType(result))
			{
				return (IEnumerable)result;
			}
			if (modelType == null || modelType.IsInstanceOfType(result))
			{
				return new object[]
				{
					result
				};
			}
			if (isAsyncSelect)
			{
				throw new InvalidOperationException(SR.GetString("ModelDataSourceView_InvalidAsyncSelectReturnType", new object[]
				{
					modelType
				}));
			}
			throw new InvalidOperationException(SR.GetString("ModelDataSourceView_InvalidSelectReturnType", new object[]
			{
				modelType
			}));
		}

		// Token: 0x060038D5 RID: 14549 RVA: 0x000B8D88 File Offset: 0x000B6F88
		private static bool IsCancellationRequired(MethodInfo method, out string parameterName)
		{
			parameterName = null;
			bool result = false;
			ParameterInfo parameterInfo = method.GetParameters().LastOrDefault<ParameterInfo>();
			if (parameterInfo != null && parameterInfo.ParameterType == typeof(CancellationToken))
			{
				result = true;
				parameterName = parameterInfo.Name;
			}
			return result;
		}

		// Token: 0x060038D6 RID: 14550 RVA: 0x000B8DCC File Offset: 0x000B6FCC
		private void SetCancellationTokenIfRequired(ModelDataSourceMethod method, bool isAsyncMethod, CancellationToken? cancellationToken)
		{
			string key;
			if (isAsyncMethod && ModelDataSourceView.IsCancellationRequired(method.MethodInfo, out key))
			{
				if (cancellationToken == null)
				{
					throw new InvalidOperationException(SR.GetString("ModelDataSourceView_CancellationTokenIsNotSupported"));
				}
				method.Parameters[key] = cancellationToken;
			}
		}

		// Token: 0x060038D7 RID: 14551 RVA: 0x000B8E18 File Offset: 0x000B7018
		protected virtual object GetDeleteMethodResult(IDictionary keys, IDictionary oldValues)
		{
			return this.GetDeleteMethodResult(keys, oldValues, null, false, null);
		}

		// Token: 0x060038D8 RID: 14552 RVA: 0x000B8E38 File Offset: 0x000B7038
		private object GetDeleteMethodResult(IDictionary keys, IDictionary oldValues, ModelDataSourceMethod method, bool isAsyncMethod, CancellationToken? cancellationToken)
		{
			method = ((method == null) ? this.EvaluateDeleteMethodParameters(keys, oldValues) : this.EvaluateDeleteMethodParameters(keys, oldValues, method));
			this.SetCancellationTokenIfRequired(method, isAsyncMethod, cancellationToken);
			ModelDataMethodResult modelDataMethodResult = this.InvokeMethod(method, isAsyncMethod);
			return modelDataMethodResult.ReturnValue;
		}

		// Token: 0x060038D9 RID: 14553 RVA: 0x000B8E78 File Offset: 0x000B7078
		protected virtual ModelDataSourceMethod EvaluateDeleteMethodParameters(IDictionary keys, IDictionary oldValues)
		{
			return this.EvaluateDeleteMethodParameters(keys, oldValues, null);
		}

		// Token: 0x060038DA RID: 14554 RVA: 0x000B8E84 File Offset: 0x000B7084
		private ModelDataSourceMethod EvaluateDeleteMethodParameters(IDictionary keys, IDictionary oldValues, ModelDataSourceMethod method)
		{
			if (!this.CanDelete)
			{
				throw new NotSupportedException(SR.GetString("ModelDataSourceView_DeleteNotSupported"));
			}
			IDictionary dictionary = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
			ModelDataSourceView.MergeDictionaries(keys, dictionary);
			ModelDataSourceView.MergeDictionaries(oldValues, dictionary);
			method = (method ?? this.FindMethod(this.DeleteMethod));
			this.EvaluateMethodParameters(DataSourceOperation.Delete, method, dictionary);
			return method;
		}

		// Token: 0x060038DB RID: 14555 RVA: 0x000B8EE0 File Offset: 0x000B70E0
		protected virtual object GetInsertMethodResult(IDictionary values)
		{
			return this.GetInsertMethodResult(values, null, false, null);
		}

		// Token: 0x060038DC RID: 14556 RVA: 0x000B8F00 File Offset: 0x000B7100
		private object GetInsertMethodResult(IDictionary values, ModelDataSourceMethod method, bool isAsyncMethod, CancellationToken? cancellationToken)
		{
			method = ((method == null) ? this.EvaluateInsertMethodParameters(values) : this.EvaluateInsertMethodParameters(values, method));
			this.SetCancellationTokenIfRequired(method, isAsyncMethod, cancellationToken);
			ModelDataMethodResult modelDataMethodResult = this.InvokeMethod(method, isAsyncMethod);
			return modelDataMethodResult.ReturnValue;
		}

		// Token: 0x060038DD RID: 14557 RVA: 0x000B8F3C File Offset: 0x000B713C
		protected virtual ModelDataSourceMethod EvaluateInsertMethodParameters(IDictionary values)
		{
			return this.EvaluateInsertMethodParameters(values, null);
		}

		// Token: 0x060038DE RID: 14558 RVA: 0x000B8F48 File Offset: 0x000B7148
		private ModelDataSourceMethod EvaluateInsertMethodParameters(IDictionary values, ModelDataSourceMethod method)
		{
			if (!this.CanInsert)
			{
				throw new NotSupportedException(SR.GetString("ModelDataSourceView_InsertNotSupported"));
			}
			IDictionary dictionary = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
			ModelDataSourceView.MergeDictionaries(values, dictionary);
			method = (method ?? this.FindMethod(this.InsertMethod));
			this.EvaluateMethodParameters(DataSourceOperation.Insert, method, dictionary);
			return method;
		}

		// Token: 0x060038DF RID: 14559 RVA: 0x000B8F9C File Offset: 0x000B719C
		protected virtual object GetUpdateMethodResult(IDictionary keys, IDictionary values, IDictionary oldValues)
		{
			return this.GetUpdateMethodResult(keys, values, oldValues, null, false, null);
		}

		// Token: 0x060038E0 RID: 14560 RVA: 0x000B8FC0 File Offset: 0x000B71C0
		private object GetUpdateMethodResult(IDictionary keys, IDictionary values, IDictionary oldValues, ModelDataSourceMethod method, bool isAsyncMethod, CancellationToken? cancellationToken)
		{
			method = ((method == null) ? this.EvaluateUpdateMethodParameters(keys, values, oldValues) : this.EvaluateUpdateMethodParameters(keys, values, oldValues, method));
			this.SetCancellationTokenIfRequired(method, isAsyncMethod, cancellationToken);
			ModelDataMethodResult modelDataMethodResult = this.InvokeMethod(method, isAsyncMethod);
			return modelDataMethodResult.ReturnValue;
		}

		// Token: 0x060038E1 RID: 14561 RVA: 0x000B9006 File Offset: 0x000B7206
		protected virtual ModelDataSourceMethod EvaluateUpdateMethodParameters(IDictionary keys, IDictionary values, IDictionary oldValues)
		{
			return this.EvaluateUpdateMethodParameters(keys, values, oldValues, null);
		}

		// Token: 0x060038E2 RID: 14562 RVA: 0x000B9014 File Offset: 0x000B7214
		private ModelDataSourceMethod EvaluateUpdateMethodParameters(IDictionary keys, IDictionary values, IDictionary oldValues, ModelDataSourceMethod method)
		{
			if (!this.CanUpdate)
			{
				throw new NotSupportedException(SR.GetString("ModelDataSourceView_UpdateNotSupported"));
			}
			IDictionary dictionary = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
			ModelDataSourceView.MergeDictionaries(oldValues, dictionary);
			ModelDataSourceView.MergeDictionaries(keys, dictionary);
			ModelDataSourceView.MergeDictionaries(values, dictionary);
			method = (method ?? this.FindMethod(this.UpdateMethod));
			this.EvaluateMethodParameters(DataSourceOperation.Update, method, dictionary);
			return method;
		}

		// Token: 0x060038E3 RID: 14563 RVA: 0x000B9079 File Offset: 0x000B7279
		private static int GetIntegerReturnValue(object result)
		{
			if (!(result is int))
			{
				return -1;
			}
			return (int)result;
		}

		// Token: 0x170010A1 RID: 4257
		// (get) Token: 0x060038E4 RID: 14564 RVA: 0x000B908C File Offset: 0x000B728C
		internal bool IsSelectMethodAsync
		{
			get
			{
				ModelDataSourceMethod modelDataSourceMethod;
				return this.RequireAsyncModelBinding(this.SelectMethod, out modelDataSourceMethod);
			}
		}

		// Token: 0x060038E5 RID: 14565 RVA: 0x000B90A8 File Offset: 0x000B72A8
		public override void Select(DataSourceSelectArguments arguments, DataSourceViewSelectCallback callback)
		{
			ModelDataSourceMethod method;
			if (this.RequireAsyncModelBinding(this.SelectMethod, out method))
			{
				this.SelectAsync(arguments, callback, method);
				return;
			}
			base.Select(arguments, callback);
		}

		// Token: 0x060038E6 RID: 14566 RVA: 0x000B90D8 File Offset: 0x000B72D8
		private void SelectAsync(DataSourceSelectArguments arguments, DataSourceViewSelectCallback callback, ModelDataSourceMethod method)
		{
			Func<object, Task> selectAsyncFunc = this.GetSelectAsyncFunc(arguments, callback, method);
			AspNetSynchronizationContext aspNetSynchronizationContext = this._owner.DataControl.Page.Context.SyncContext as AspNetSynchronizationContext;
			if (aspNetSynchronizationContext == null)
			{
				throw new InvalidOperationException(SR.GetString("ModelDataSourceView_UseAsyncMethodMustBeUsingAsyncPage"));
			}
			aspNetSynchronizationContext.PostAsync(selectAsyncFunc, null);
		}

		// Token: 0x060038E7 RID: 14567 RVA: 0x000B912C File Offset: 0x000B732C
		private Func<object, Task> GetSelectAsyncFunc(DataSourceSelectArguments arguments, DataSourceViewSelectCallback callback, ModelDataSourceMethod method)
		{
			ModelDataSourceView.<>c__DisplayClass86_0 CS$<>8__locals1 = new ModelDataSourceView.<>c__DisplayClass86_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.arguments = arguments;
			CS$<>8__locals1.method = method;
			CS$<>8__locals1.callback = callback;
			return delegate(object _)
			{
				ModelDataSourceView.<>c__DisplayClass86_0.<<GetSelectAsyncFunc>b__0>d <<GetSelectAsyncFunc>b__0>d;
				<<GetSelectAsyncFunc>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<GetSelectAsyncFunc>b__0>d.<>4__this = CS$<>8__locals1;
				<<GetSelectAsyncFunc>b__0>d.<>1__state = -1;
				<<GetSelectAsyncFunc>b__0>d.<>t__builder.Start<ModelDataSourceView.<>c__DisplayClass86_0.<<GetSelectAsyncFunc>b__0>d>(ref <<GetSelectAsyncFunc>b__0>d);
				return <<GetSelectAsyncFunc>b__0>d.<>t__builder.Task;
			};
		}

		// Token: 0x060038E8 RID: 14568 RVA: 0x000B916C File Offset: 0x000B736C
		public override void Insert(IDictionary values, DataSourceViewOperationCallback callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			ModelDataSourceMethod method;
			if (this.RequireAsyncModelBinding(this.InsertMethod, out method))
			{
				this.ViewOperationAsync((CancellationToken cancellationToken) => (Task)this.GetInsertMethodResult(values, method, true, new CancellationToken?(cancellationToken)), callback);
				return;
			}
			base.Insert(values, callback);
		}

		// Token: 0x060038E9 RID: 14569 RVA: 0x000B91D0 File Offset: 0x000B73D0
		public override void Update(IDictionary keys, IDictionary values, IDictionary oldValues, DataSourceViewOperationCallback callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			ModelDataSourceMethod method;
			if (this.RequireAsyncModelBinding(this.UpdateMethod, out method))
			{
				this.ViewOperationAsync((CancellationToken cancellationToken) => (Task)this.GetUpdateMethodResult(keys, values, oldValues, method, true, new CancellationToken?(cancellationToken)), callback);
				return;
			}
			base.Update(keys, values, oldValues, callback);
		}

		// Token: 0x060038EA RID: 14570 RVA: 0x000B9254 File Offset: 0x000B7454
		public override void Delete(IDictionary keys, IDictionary oldValues, DataSourceViewOperationCallback callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			ModelDataSourceMethod method;
			if (this.RequireAsyncModelBinding(this.DeleteMethod, out method))
			{
				this.ViewOperationAsync((CancellationToken cancellationToken) => (Task)this.GetDeleteMethodResult(keys, oldValues, method, true, new CancellationToken?(cancellationToken)), callback);
				return;
			}
			base.Delete(keys, oldValues, callback);
		}

		// Token: 0x060038EB RID: 14571 RVA: 0x000B92C8 File Offset: 0x000B74C8
		private void ViewOperationAsync(Func<CancellationToken, Task> asyncViewOperation, DataSourceViewOperationCallback callback)
		{
			ModelDataSourceView.<>c__DisplayClass90_0 CS$<>8__locals1 = new ModelDataSourceView.<>c__DisplayClass90_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.asyncViewOperation = asyncViewOperation;
			CS$<>8__locals1.callback = callback;
			this.ValidateAsyncModelBindingRequirements();
			Func<object, Task> callback2 = delegate(object _)
			{
				ModelDataSourceView.<>c__DisplayClass90_0.<<ViewOperationAsync>b__0>d <<ViewOperationAsync>b__0>d;
				<<ViewOperationAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<ViewOperationAsync>b__0>d.<>4__this = CS$<>8__locals1;
				<<ViewOperationAsync>b__0>d.<>1__state = -1;
				<<ViewOperationAsync>b__0>d.<>t__builder.Start<ModelDataSourceView.<>c__DisplayClass90_0.<<ViewOperationAsync>b__0>d>(ref <<ViewOperationAsync>b__0>d);
				return <<ViewOperationAsync>b__0>d.<>t__builder.Task;
			};
			AspNetSynchronizationContext aspNetSynchronizationContext = this._owner.DataControl.Page.Context.SyncContext as AspNetSynchronizationContext;
			if (aspNetSynchronizationContext == null)
			{
				throw new InvalidOperationException(SR.GetString("ModelDataSourceView_UseAsyncMethodMustBeUsingAsyncPage"));
			}
			aspNetSynchronizationContext.PostAsync(callback2, null);
		}

		// Token: 0x060038EC RID: 14572 RVA: 0x000B9340 File Offset: 0x000B7540
		protected override int ExecuteDelete(IDictionary keys, IDictionary oldValues)
		{
			object deleteMethodResult = this.GetDeleteMethodResult(keys, oldValues);
			this.OnDataSourceViewChanged(EventArgs.Empty);
			return ModelDataSourceView.GetIntegerReturnValue(deleteMethodResult);
		}

		// Token: 0x060038ED RID: 14573 RVA: 0x000B9368 File Offset: 0x000B7568
		protected override int ExecuteInsert(IDictionary values)
		{
			object insertMethodResult = this.GetInsertMethodResult(values);
			if (this._owner.DataControl.Page.ModelState.IsValid)
			{
				this.OnDataSourceViewChanged(EventArgs.Empty);
			}
			return ModelDataSourceView.GetIntegerReturnValue(insertMethodResult);
		}

		// Token: 0x060038EE RID: 14574 RVA: 0x000B93AC File Offset: 0x000B75AC
		protected internal override IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
		{
			object selectMethodResult = this.GetSelectMethodResult(arguments);
			return this.CreateSelectResult(selectMethodResult);
		}

		// Token: 0x060038EF RID: 14575 RVA: 0x000B93C8 File Offset: 0x000B75C8
		protected override int ExecuteUpdate(IDictionary keys, IDictionary values, IDictionary oldValues)
		{
			object updateMethodResult = this.GetUpdateMethodResult(keys, values, oldValues);
			if (this._owner.DataControl.Page.ModelState.IsValid)
			{
				this.OnDataSourceViewChanged(EventArgs.Empty);
			}
			return ModelDataSourceView.GetIntegerReturnValue(updateMethodResult);
		}

		// Token: 0x060038F0 RID: 14576 RVA: 0x000B940C File Offset: 0x000B760C
		internal IEnumerable Select(DataSourceSelectArguments arguments)
		{
			return this.ExecuteSelect(arguments);
		}

		// Token: 0x060038F1 RID: 14577 RVA: 0x000B9415 File Offset: 0x000B7615
		internal int Update(IDictionary keys, IDictionary values, IDictionary oldValues)
		{
			return this.ExecuteUpdate(keys, values, oldValues);
		}

		// Token: 0x060038F2 RID: 14578 RVA: 0x000B9420 File Offset: 0x000B7620
		internal Func<object, Task> SelectAsyncInternal(DataSourceSelectArguments arguments, DataSourceViewSelectCallback callback, ModelDataSourceMethod method)
		{
			return this.GetSelectAsyncFunc(arguments, callback, method);
		}

		// Token: 0x060038F3 RID: 14579 RVA: 0x000B942C File Offset: 0x000B762C
		private void EvaluateSelectParameters()
		{
			if (!string.IsNullOrEmpty(this.SelectMethod))
			{
				ModelDataSourceMethod modelDataSourceMethod = this.FindMethod(this.SelectMethod);
				this.EvaluateMethodParameters(DataSourceOperation.Select, modelDataSourceMethod, null, true);
			}
		}

		// Token: 0x060038F4 RID: 14580 RVA: 0x000B945D File Offset: 0x000B765D
		protected virtual void EvaluateMethodParameters(DataSourceOperation dataSourceOperation, ModelDataSourceMethod modelDataSourceMethod, IDictionary controlValues)
		{
			this.EvaluateMethodParameters(dataSourceOperation, modelDataSourceMethod, controlValues, false);
		}

		// Token: 0x060038F5 RID: 14581 RVA: 0x000B946C File Offset: 0x000B766C
		protected virtual void EvaluateMethodParameters(DataSourceOperation dataSourceOperation, ModelDataSourceMethod modelDataSourceMethod, IDictionary controlValues, bool isPageLoadComplete)
		{
			MethodInfo methodInfo = modelDataSourceMethod.MethodInfo;
			IModelBinder defaultBinder = ModelBinders.Binders.DefaultBinder;
			IValueProvider valueProviderFromDictionary = ModelDataSourceView.GetValueProviderFromDictionary(controlValues);
			ModelBindingExecutionContext modelBindingExecutionContext = this._owner.DataControl.Page.ModelBindingExecutionContext;
			Control control = null;
			if (BinaryCompatibility.Current.TargetsAtLeastFramework46)
			{
				Control control2 = modelBindingExecutionContext.TryGetService<Control>();
				if (control2 != this._owner.DataControl)
				{
					control = control2;
				}
			}
			modelBindingExecutionContext.PublishService<Control>(this._owner.DataControl);
			if (dataSourceOperation != DataSourceOperation.Select)
			{
				this._owner.DataControl.Page.SetActiveValueProvider(valueProviderFromDictionary);
			}
			ParameterInfo[] parameters = methodInfo.GetParameters();
			ParameterInfo parameterInfo = null;
			if (parameters.Length != 0)
			{
				parameterInfo = parameters[parameters.Length - 1];
			}
			ParameterInfo[] array = parameters;
			int i = 0;
			while (i < array.Length)
			{
				ParameterInfo parameterInfo2 = array[i];
				object obj = null;
				string name = parameterInfo2.Name;
				if (parameterInfo2.ParameterType == typeof(ModelMethodContext))
				{
					obj = new ModelMethodContext(this._owner.DataControl.Page);
					goto IL_24A;
				}
				if (parameterInfo2.IsOut)
				{
					goto IL_24A;
				}
				bool validateRequest;
				IValueProvider customValueProvider = this.GetCustomValueProvider(modelBindingExecutionContext, parameterInfo2, ref name, out validateRequest);
				ModelStateDictionary modelState = isPageLoadComplete ? new ModelStateDictionary() : this._owner.DataControl.Page.ModelState;
				ModelBindingContext modelBindingContext = new ModelBindingContext
				{
					ModelBinderProviders = ModelBinderProviders.Providers,
					ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, parameterInfo2.ParameterType),
					ModelState = modelState,
					ModelName = name,
					ValueProvider = customValueProvider,
					ValidateRequest = validateRequest
				};
				if (dataSourceOperation == DataSourceOperation.Select && customValueProvider != null && parameterInfo2.ParameterType.IsSerializable)
				{
					if (!this.SelectParameters.ContainsKey(parameterInfo2.Name))
					{
						this.SelectParameters.Add(parameterInfo2.Name, new MethodParameterValue());
					}
					if (defaultBinder.BindModel(modelBindingExecutionContext, modelBindingContext))
					{
						obj = modelBindingContext.Model;
					}
					this.SelectParameters[parameterInfo2.Name].UpdateValue(obj);
				}
				else
				{
					if (isPageLoadComplete)
					{
						goto IL_25E;
					}
					if (customValueProvider == null)
					{
						modelBindingContext.ValueProvider = valueProviderFromDictionary;
					}
					if (defaultBinder.BindModel(modelBindingExecutionContext, modelBindingContext))
					{
						obj = modelBindingContext.Model;
					}
				}
				if (parameterInfo2 == parameterInfo && typeof(CancellationToken) == parameterInfo2.ParameterType && obj == null)
				{
					obj = CancellationToken.None;
				}
				if (!isPageLoadComplete)
				{
					this.ValidateParameterValue(parameterInfo2, obj, methodInfo);
					goto IL_24A;
				}
				goto IL_24A;
				IL_25E:
				i++;
				continue;
				IL_24A:
				modelDataSourceMethod.Parameters.Add(parameterInfo2.Name, obj);
				goto IL_25E;
			}
			if (control != null)
			{
				modelBindingExecutionContext.PublishService<Control>(control);
			}
		}

		// Token: 0x060038F6 RID: 14582 RVA: 0x000B96F4 File Offset: 0x000B78F4
		private static IValueProvider GetValueProviderFromDictionary(IDictionary controlValues)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (controlValues != null)
			{
				foreach (object obj in controlValues)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					dictionary.Add((string)dictionaryEntry.Key, dictionaryEntry.Value);
				}
			}
			return new DictionaryValueProvider<object>(dictionary, CultureInfo.CurrentCulture);
		}

		// Token: 0x060038F7 RID: 14583 RVA: 0x000B9770 File Offset: 0x000B7970
		private IValueProvider GetCustomValueProvider(ModelBindingExecutionContext modelBindingExecutionContext, ParameterInfo parameterInfo, ref string modelName, out bool validateRequest)
		{
			validateRequest = true;
			object[] customAttributes = parameterInfo.GetCustomAttributes(typeof(IValueProviderSource), false);
			if (customAttributes.Count<object>() > 1)
			{
				throw new NotSupportedException(SR.GetString("ModelDataSourceView_MultipleValueProvidersNotSupported", new object[]
				{
					parameterInfo.Name
				}));
			}
			if (customAttributes.Count<object>() > 0)
			{
				IValueProviderSource valueProviderSource = (IValueProviderSource)customAttributes[0];
				if (valueProviderSource is IModelNameProvider)
				{
					string modelName2 = ((IModelNameProvider)valueProviderSource).GetModelName();
					if (!string.IsNullOrEmpty(modelName2))
					{
						modelName = modelName2;
					}
				}
				if (valueProviderSource is IUnvalidatedValueProviderSource)
				{
					validateRequest = ((IUnvalidatedValueProviderSource)valueProviderSource).ValidateInput;
				}
				return valueProviderSource.GetValueProvider(modelBindingExecutionContext);
			}
			return null;
		}

		// Token: 0x060038F8 RID: 14584 RVA: 0x000B980C File Offset: 0x000B7A0C
		protected virtual ModelDataSourceMethod FindMethod(string methodName)
		{
			CallingDataMethodsEventArgs callingDataMethodsEventArgs = new CallingDataMethodsEventArgs();
			this.OnCallingDataMethods(callingDataMethodsEventArgs);
			BindingFlags bindingAttr;
			object obj;
			Type type;
			if (callingDataMethodsEventArgs.DataMethodsType != null)
			{
				if (callingDataMethodsEventArgs.DataMethodsObject != null)
				{
					throw new InvalidOperationException(SR.GetString("ModelDataSourceView_MultipleModelMethodSources", new object[]
					{
						methodName
					}));
				}
				bindingAttr = (BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
				obj = null;
				type = callingDataMethodsEventArgs.DataMethodsType;
			}
			else if (callingDataMethodsEventArgs.DataMethodsObject != null)
			{
				bindingAttr = (BindingFlags.Instance | BindingFlags.Public);
				obj = callingDataMethodsEventArgs.DataMethodsObject;
				type = obj.GetType();
			}
			else
			{
				bindingAttr = (BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
				obj = this._owner.DataControl.TemplateControl;
				type = obj.GetType();
			}
			MethodInfo[] methods = type.GetMethods(bindingAttr);
			MethodInfo[] array = Array.FindAll<MethodInfo>(methods, (MethodInfo methodInfo) => methodInfo.Name.Equals(methodName, StringComparison.OrdinalIgnoreCase));
			if (array.Length != 1)
			{
				throw new InvalidOperationException(SR.GetString("ModelDataSourceView_DataMethodNotFound", new object[]
				{
					methodName,
					type
				}));
			}
			this.ValidateMethodIsCallable(array[0]);
			return new ModelDataSourceMethod(obj, array[0]);
		}

		// Token: 0x060038F9 RID: 14585 RVA: 0x000B9910 File Offset: 0x000B7B10
		private void ValidateMethodIsCallable(MethodInfo methodInfo)
		{
			if (methodInfo.ContainsGenericParameters)
			{
				throw new InvalidOperationException(SR.GetString("ModelDataSourceView_CannotCallOpenGenericMethods", new object[]
				{
					methodInfo,
					methodInfo.ReflectedType.FullName
				}));
			}
			ParameterInfo[] parameters = methodInfo.GetParameters();
			foreach (ParameterInfo parameterInfo in parameters)
			{
				if (parameterInfo.ParameterType.IsByRef && !parameterInfo.Name.Equals("totalRowCount", StringComparison.OrdinalIgnoreCase))
				{
					throw new InvalidOperationException(SR.GetString("ModelDataSourceView_CannotCallMethodsWithOutOrRefParameters", new object[]
					{
						methodInfo,
						methodInfo.ReflectedType.FullName,
						parameterInfo
					}));
				}
			}
		}

		// Token: 0x060038FA RID: 14586 RVA: 0x000B99B4 File Offset: 0x000B7BB4
		private OrderedDictionary GetOutputParameters(ParameterInfo[] parameters, object[] values)
		{
			OrderedDictionary orderedDictionary = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
			for (int i = 0; i < parameters.Length; i++)
			{
				ParameterInfo parameterInfo = parameters[i];
				if (parameterInfo.ParameterType.IsByRef)
				{
					orderedDictionary[parameterInfo.Name] = values[i];
				}
			}
			return orderedDictionary;
		}

		// Token: 0x060038FB RID: 14587 RVA: 0x000B99FB File Offset: 0x000B7BFB
		protected virtual ModelDataMethodResult InvokeMethod(ModelDataSourceMethod method)
		{
			return this.InvokeMethod(method, false);
		}

		// Token: 0x060038FC RID: 14588 RVA: 0x000B9A08 File Offset: 0x000B7C08
		private ModelDataMethodResult InvokeMethod(ModelDataSourceMethod method, bool isAsyncMethod)
		{
			object[] array = null;
			if (method.Parameters != null && method.Parameters.Count > 0)
			{
				array = new object[method.Parameters.Count];
				for (int i = 0; i < method.Parameters.Count; i++)
				{
					array[i] = method.Parameters[i];
				}
			}
			object returnValue = ModelDataSourceView._methodInvokerDispatcher(method.MethodInfo, method.Instance, array);
			OrderedDictionary outputParameters = this.GetOutputParameters(method.MethodInfo.GetParameters(), array);
			method.Instance = null;
			if (!isAsyncMethod)
			{
				this._owner.DataControl.Page.SetActiveValueProvider(null);
			}
			return new ModelDataMethodResult(returnValue, outputParameters);
		}

		// Token: 0x060038FD RID: 14589 RVA: 0x000B9AB7 File Offset: 0x000B7CB7
		protected virtual bool IsTrackingViewState()
		{
			return this._tracking;
		}

		// Token: 0x060038FE RID: 14590 RVA: 0x000B9ABF File Offset: 0x000B7CBF
		protected virtual void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				((IStateManager)this.SelectParameters).LoadViewState(savedState);
			}
		}

		// Token: 0x060038FF RID: 14591 RVA: 0x000B9AD0 File Offset: 0x000B7CD0
		protected virtual object SaveViewState()
		{
			if (this._selectParameters == null)
			{
				return null;
			}
			return ((IStateManager)this._selectParameters).SaveViewState();
		}

		// Token: 0x06003900 RID: 14592 RVA: 0x000B9AE7 File Offset: 0x000B7CE7
		protected virtual void TrackViewState()
		{
			this._tracking = true;
			if (this._selectParameters != null)
			{
				((IStateManager)this._selectParameters).TrackViewState();
			}
		}

		// Token: 0x06003901 RID: 14593 RVA: 0x000B9B04 File Offset: 0x000B7D04
		private void ValidateParameterValue(ParameterInfo parameterInfo, object value, MethodInfo methodInfo)
		{
			if (value == null && !TypeHelpers.TypeAllowsNullValue(parameterInfo.ParameterType))
			{
				string message = string.Format(CultureInfo.CurrentCulture, SR.GetString("ModelDataSourceView_ParameterCannotBeNull"), new object[]
				{
					parameterInfo.Name,
					parameterInfo.ParameterType,
					methodInfo,
					methodInfo.DeclaringType
				});
				throw new InvalidOperationException(message);
			}
			if (value != null && !parameterInfo.ParameterType.IsInstanceOfType(value))
			{
				string message2 = string.Format(CultureInfo.CurrentCulture, SR.GetString("ModelDataSourceView_ParameterValueHasWrongType"), new object[]
				{
					parameterInfo.Name,
					methodInfo,
					methodInfo.DeclaringType,
					value.GetType(),
					parameterInfo.ParameterType
				});
				throw new InvalidOperationException(message2);
			}
		}

		// Token: 0x06003902 RID: 14594 RVA: 0x000B9BC0 File Offset: 0x000B7DC0
		private static void MergeDictionaries(IDictionary source, IDictionary destination)
		{
			if (source != null)
			{
				foreach (object obj in source)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					object value = dictionaryEntry.Value;
					string key = (string)dictionaryEntry.Key;
					destination[key] = value;
				}
			}
		}

		// Token: 0x170010A2 RID: 4258
		// (get) Token: 0x06003903 RID: 14595 RVA: 0x000B9C30 File Offset: 0x000B7E30
		private Type ModelType
		{
			get
			{
				string modelTypeName = this.ModelTypeName;
				if (string.IsNullOrEmpty(modelTypeName))
				{
					return null;
				}
				return BuildManager.GetType(modelTypeName, true, true);
			}
		}

		// Token: 0x170010A3 RID: 4259
		// (get) Token: 0x06003904 RID: 14596 RVA: 0x000B9C58 File Offset: 0x000B7E58
		private MethodParametersDictionary SelectParameters
		{
			get
			{
				if (this._selectParameters == null)
				{
					this._selectParameters = new MethodParametersDictionary();
					this._selectParameters.ParametersChanged += this.OnSelectParametersChanged;
					if (this._tracking)
					{
						((IStateManager)this._selectParameters).TrackViewState();
					}
				}
				return this._selectParameters;
			}
		}

		// Token: 0x06003905 RID: 14597 RVA: 0x000B9CA8 File Offset: 0x000B7EA8
		private void OnSelectParametersChanged(object sender, EventArgs e)
		{
			this.OnDataSourceViewChanged(EventArgs.Empty);
		}

		// Token: 0x170010A4 RID: 4260
		// (get) Token: 0x06003906 RID: 14598 RVA: 0x000B9CB5 File Offset: 0x000B7EB5
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState();
			}
		}

		// Token: 0x06003907 RID: 14599 RVA: 0x000B9CBD File Offset: 0x000B7EBD
		void IStateManager.LoadViewState(object savedState)
		{
			this.LoadViewState(savedState);
		}

		// Token: 0x06003908 RID: 14600 RVA: 0x000B9CC6 File Offset: 0x000B7EC6
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x06003909 RID: 14601 RVA: 0x000B9CCE File Offset: 0x000B7ECE
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x04002294 RID: 8852
		private static readonly ModelDataSourceView.MethodInvokerDispatcher _methodInvokerDispatcher = ((MethodInfo methodInfo, object instance, object[] args) => methodInfo.Invoke(instance, args)).Compile();

		// Token: 0x04002295 RID: 8853
		private ModelDataSource _owner;

		// Token: 0x04002296 RID: 8854
		private MethodParametersDictionary _selectParameters;

		// Token: 0x04002297 RID: 8855
		private bool _tracking;

		// Token: 0x04002298 RID: 8856
		private string _modelTypeName;

		// Token: 0x04002299 RID: 8857
		private string _deleteMethod;

		// Token: 0x0400229A RID: 8858
		private string _insertMethod;

		// Token: 0x0400229B RID: 8859
		private string _selectMethod;

		// Token: 0x0400229C RID: 8860
		private string _updateMethod;

		// Token: 0x0400229D RID: 8861
		private string _dataKeyName;

		// Token: 0x0400229E RID: 8862
		private Task _viewOperationTask;

		// Token: 0x0400229F RID: 8863
		private const string TotalRowCountParameterName = "totalRowCount";

		// Token: 0x040022A0 RID: 8864
		private const string MaximumRowsParameterName = "maximumRows";

		// Token: 0x040022A1 RID: 8865
		private const string StartRowIndexParameterName = "startRowIndex";

		// Token: 0x040022A2 RID: 8866
		private const string SortParameterName = "sortByExpression";

		// Token: 0x040022A3 RID: 8867
		private static readonly object EventCallingDataMethods = new object();

		// Token: 0x020009B1 RID: 2481
		// (Invoke) Token: 0x06006BEF RID: 27631
		private delegate object MethodInvokerDispatcher(MethodInfo methodInfo, object instance, object[] args);
	}
}
