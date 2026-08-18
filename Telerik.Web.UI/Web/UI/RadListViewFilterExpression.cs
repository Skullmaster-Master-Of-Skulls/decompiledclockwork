using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001973 RID: 6515
	[Serializable]
	public abstract class RadListViewFilterExpression : IStateManager, IHideObjectMembers, IEquatable<RadListViewFilterExpression>
	{
		// Token: 0x0600FC3F RID: 64575 RVA: 0x0038D3DE File Offset: 0x0038B5DE
		protected virtual object ExtractFieldValueFromItem(object item, string fieldName)
		{
			return DataBinder.Eval(item, fieldName);
		}

		// Token: 0x17004C30 RID: 19504
		// (get) Token: 0x0600FC40 RID: 64576 RVA: 0x0038D3E7 File Offset: 0x0038B5E7
		protected virtual StateBag ViewState
		{
			get
			{
				if (this._viewState == null)
				{
					this._viewState = new StateBag(true);
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._viewState).TrackViewState();
					}
				}
				return this._viewState;
			}
		}

		// Token: 0x0600FC41 RID: 64577 RVA: 0x0038D418 File Offset: 0x0038B618
		protected virtual bool IsValidStringValue(string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				foreach (string value2 in RadListViewFilterExpression.IllegalStrings)
				{
					if (value.IndexOf(value2, StringComparison.OrdinalIgnoreCase) > -1)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x17004C31 RID: 19505
		// (get) Token: 0x0600FC42 RID: 64578 RVA: 0x0038D458 File Offset: 0x0038B658
		// (set) Token: 0x0600FC43 RID: 64579 RVA: 0x0038D488 File Offset: 0x0038B688
		[DefaultValue("")]
		public virtual string FieldName
		{
			get
			{
				object obj = this.ViewState["FieldName"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentNullException("value cannot be null");
				}
				if (!((IStateManager)this.ViewState).IsTrackingViewState && !string.IsNullOrEmpty(value))
				{
					((IStateManager)this.ViewState).TrackViewState();
				}
				this.ViewState["FieldName"] = value;
			}
		}

		// Token: 0x17004C32 RID: 19506
		// (get) Token: 0x0600FC44 RID: 64580 RVA: 0x0038D4DC File Offset: 0x0038B6DC
		// (set) Token: 0x0600FC45 RID: 64581 RVA: 0x0038D54B File Offset: 0x0038B74B
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		[Browsable(false)]
		public static string[] IllegalStrings
		{
			get
			{
				if (RadListViewFilterExpression._illegalStrings == null)
				{
					RadListViewFilterExpression._illegalStrings = new string[]
					{
						" LIKE ",
						" AND ",
						" OR ",
						"\"",
						">",
						"<",
						"<>",
						" NULL ",
						" IS "
					};
				}
				return RadListViewFilterExpression._illegalStrings;
			}
			set
			{
				RadListViewFilterExpression._illegalStrings = value;
			}
		}

		// Token: 0x17004C33 RID: 19507
		// (get) Token: 0x0600FC46 RID: 64582
		[Browsable(false)]
		public abstract RadListViewFilterFunction FilterFunction { get; }

		// Token: 0x17004C34 RID: 19508
		// (get) Token: 0x0600FC47 RID: 64583
		[Browsable(false)]
		public abstract Type FieldType { get; }

		// Token: 0x17004C35 RID: 19509
		// (get) Token: 0x0600FC48 RID: 64584 RVA: 0x0038D553 File Offset: 0x0038B753
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual string ExpressionType
		{
			get
			{
				return base.GetType().Name;
			}
		}

		// Token: 0x0600FC49 RID: 64585
		public abstract Predicate<object> ToPredicate();

		// Token: 0x0600FC4A RID: 64586
		public abstract string ToDynamicLinq();

		// Token: 0x0600FC4B RID: 64587
		public abstract string ToEntitySQL();

		// Token: 0x0600FC4C RID: 64588
		public abstract string ToOql();

		// Token: 0x0600FC4D RID: 64589 RVA: 0x0038D560 File Offset: 0x0038B760
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual bool Equals(RadListViewFilterExpression other)
		{
			return !object.ReferenceEquals(null, other) && (object.ReferenceEquals(this, other) || object.Equals(other.FieldName, this.FieldName));
		}

		// Token: 0x0600FC4E RID: 64590 RVA: 0x0038D589 File Offset: 0x0038B789
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && (object.ReferenceEquals(this, obj) || (!obj.GetType().IsAssignableFrom(typeof(RadListViewFilterExpression)) && this.Equals((RadListViewFilterExpression)obj)));
		}

		// Token: 0x0600FC4F RID: 64591 RVA: 0x0038D5C6 File Offset: 0x0038B7C6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			if (this.FieldName == null)
			{
				return 0;
			}
			return this.FieldName.GetHashCode();
		}

		// Token: 0x17004C36 RID: 19510
		// (get) Token: 0x0600FC50 RID: 64592 RVA: 0x0038D5DD File Offset: 0x0038B7DD
		protected virtual bool IsTrackingViewState
		{
			get
			{
				return this._isTrackingViewState;
			}
		}

		// Token: 0x0600FC51 RID: 64593 RVA: 0x0038D5E5 File Offset: 0x0038B7E5
		void IStateManager.LoadViewState(object state)
		{
			this.LoadViewState(state);
		}

		// Token: 0x0600FC52 RID: 64594 RVA: 0x0038D5EE File Offset: 0x0038B7EE
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x0600FC53 RID: 64595 RVA: 0x0038D5F6 File Offset: 0x0038B7F6
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x17004C37 RID: 19511
		// (get) Token: 0x0600FC54 RID: 64596 RVA: 0x0038D5FE File Offset: 0x0038B7FE
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		// Token: 0x0600FC55 RID: 64597 RVA: 0x0038D608 File Offset: 0x0038B808
		protected virtual void LoadViewState(object state)
		{
			object[] array = state as object[];
			if (array != null && array.Length > 0)
			{
				((IStateManager)this.ViewState).LoadViewState(array[0]);
			}
		}

		// Token: 0x0600FC56 RID: 64598 RVA: 0x0038D634 File Offset: 0x0038B834
		protected virtual object SaveViewState()
		{
			ArrayList arrayList = new ArrayList
			{
				((IStateManager)this.ViewState).SaveViewState()
			};
			return arrayList.ToArray(typeof(object));
		}

		// Token: 0x0600FC57 RID: 64599 RVA: 0x0038D66B File Offset: 0x0038B86B
		protected virtual void TrackViewState()
		{
			this._isTrackingViewState = true;
			((IStateManager)this.ViewState).TrackViewState();
		}

		// Token: 0x0600FC58 RID: 64600 RVA: 0x0038D680 File Offset: 0x0038B880
		internal static RadListViewFilterExpression CreateExpressionFromTypeName(string expressionTypeName, string expressionFieldType)
		{
			RadListViewFilterExpression result = null;
			if (expressionTypeName.StartsWith("RadListViewEqualToFilterExpression"))
			{
				result = RadListViewFilterExpression.ConstructExpressionInstance(typeof(RadListViewEqualToFilterExpression<>), expressionFieldType);
			}
			else if (expressionTypeName.StartsWith("RadListViewNotEqualToFilterExpression"))
			{
				result = RadListViewFilterExpression.ConstructExpressionInstance(typeof(RadListViewNotEqualToFilterExpression<>), expressionFieldType);
			}
			else if (expressionTypeName.StartsWith("RadListViewGreaterThanFilterExpression"))
			{
				result = RadListViewFilterExpression.ConstructExpressionInstance(typeof(RadListViewGreaterThanFilterExpression<>), expressionFieldType);
			}
			else if (expressionTypeName.StartsWith("RadListViewGreaterThenOrEqualToFilterExpression"))
			{
				result = RadListViewFilterExpression.ConstructExpressionInstance(typeof(RadListViewGreaterThenOrEqualToFilterExpression<>), expressionFieldType);
			}
			else if (expressionTypeName.StartsWith("RadListViewLessThanFilterExpression"))
			{
				result = RadListViewFilterExpression.ConstructExpressionInstance(typeof(RadListViewLessThanFilterExpression<>), expressionFieldType);
			}
			else if (expressionTypeName.StartsWith("RadListViewLessThanOrEqualToFilterExpression"))
			{
				result = RadListViewFilterExpression.ConstructExpressionInstance(typeof(RadListViewLessThanOrEqualToFilterExpression<>), expressionFieldType);
			}
			else if (expressionTypeName.StartsWith("RadListViewContainsFilterExpression"))
			{
				result = new RadListViewContainsFilterExpression();
			}
			else if (expressionTypeName.StartsWith("RadListViewStartsWithFilterExpression"))
			{
				result = new RadListViewStartsWithFilterExpression();
			}
			else if (expressionTypeName.StartsWith("RadListViewEndsWithFilterExpression"))
			{
				result = new RadListViewEndsWithFilterExpression();
			}
			else if (expressionTypeName.StartsWith("RadListViewIsNullFilterExpression"))
			{
				result = new RadListViewIsNullFilterExpression();
			}
			else if (expressionTypeName.StartsWith("RadListViewIsNotNullFilterExpression"))
			{
				result = new RadListViewIsNotNullFilterExpression();
			}
			else if (expressionTypeName.StartsWith("RadListViewIsEmptyFilterExpression"))
			{
				result = new RadListViewIsEmptyFilterExpression();
			}
			else if (expressionTypeName.StartsWith("RadListViewIsNotEmptyFilterExpression"))
			{
				result = new RadListViewIsNotEmptyFilterExpression();
			}
			else if (expressionTypeName.StartsWith("RadListViewGroupFilterExpression"))
			{
				result = new RadListViewGroupFilterExpression();
			}
			return result;
		}

		// Token: 0x0600FC59 RID: 64601 RVA: 0x0038D80C File Offset: 0x0038BA0C
		protected static RadListViewFilterExpression ConstructExpressionInstance(Type expressionType, string expressionFieldType)
		{
			Type type = Type.GetType(expressionFieldType);
			if (type != null)
			{
				Type type2 = expressionType.MakeGenericType(new Type[]
				{
					type
				});
				return (RadListViewFilterExpression)Activator.CreateInstance(type2, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, null, null);
			}
			return null;
		}

		// Token: 0x0600FC5B RID: 64603 RVA: 0x0038D858 File Offset: 0x0038BA58
		Type IHideObjectMembers.GetType()
		{
			return base.GetType();
		}

		// Token: 0x040047C1 RID: 18369
		private bool _isTrackingViewState;

		// Token: 0x040047C2 RID: 18370
		private StateBag _viewState;

		// Token: 0x040047C3 RID: 18371
		private static string[] _illegalStrings;
	}
}
