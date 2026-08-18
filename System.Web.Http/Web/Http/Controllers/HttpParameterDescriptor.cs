using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Http.Internal;
using System.Web.Http.ModelBinding;
using System.Web.Http.Properties;

namespace System.Web.Http.Controllers
{
	// Token: 0x0200011F RID: 287
	public abstract class HttpParameterDescriptor
	{
		// Token: 0x060006EC RID: 1772 RVA: 0x0001720E File Offset: 0x0001540E
		protected HttpParameterDescriptor()
		{
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00017221 File Offset: 0x00015421
		protected HttpParameterDescriptor(HttpActionDescriptor actionDescriptor)
		{
			if (actionDescriptor == null)
			{
				throw Error.ArgumentNull("actionDescriptor");
			}
			this._actionDescriptor = actionDescriptor;
			this._configuration = this._actionDescriptor.Configuration;
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x0001725A File Offset: 0x0001545A
		// (set) Token: 0x060006EF RID: 1775 RVA: 0x00017262 File Offset: 0x00015462
		public HttpConfiguration Configuration
		{
			get
			{
				return this._configuration;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._configuration = value;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x00017274 File Offset: 0x00015474
		// (set) Token: 0x060006F1 RID: 1777 RVA: 0x0001727C File Offset: 0x0001547C
		public HttpActionDescriptor ActionDescriptor
		{
			get
			{
				return this._actionDescriptor;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._actionDescriptor = value;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x0001728E File Offset: 0x0001548E
		public ConcurrentDictionary<object, object> Properties
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x00017296 File Offset: 0x00015496
		public virtual object DefaultValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060006F4 RID: 1780
		public abstract string ParameterName { get; }

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060006F5 RID: 1781
		public abstract Type ParameterType { get; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x0001729C File Offset: 0x0001549C
		public virtual string Prefix
		{
			get
			{
				ParameterBindingAttribute parameterBinderAttribute = this.ParameterBinderAttribute;
				ModelBinderAttribute modelBinderAttribute = parameterBinderAttribute as ModelBinderAttribute;
				if (modelBinderAttribute == null)
				{
					return null;
				}
				return modelBinderAttribute.Name;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x000172C2 File Offset: 0x000154C2
		public virtual bool IsOptional
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x000172C5 File Offset: 0x000154C5
		// (set) Token: 0x060006F9 RID: 1785 RVA: 0x000172F0 File Offset: 0x000154F0
		public virtual ParameterBindingAttribute ParameterBinderAttribute
		{
			get
			{
				if (this._parameterBindingAttribute == null && !this._searchedModelBinderAttribute)
				{
					this._searchedModelBinderAttribute = true;
					this._parameterBindingAttribute = this.FindParameterBindingAttribute();
				}
				return this._parameterBindingAttribute;
			}
			set
			{
				this._parameterBindingAttribute = value;
			}
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x000172F9 File Offset: 0x000154F9
		public virtual Collection<T> GetCustomAttributes<T>() where T : class
		{
			return new Collection<T>();
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00017300 File Offset: 0x00015500
		private ParameterBindingAttribute FindParameterBindingAttribute()
		{
			return HttpParameterDescriptor.ChooseAttribute(this.GetCustomAttributes<ParameterBindingAttribute>()) ?? HttpParameterDescriptor.ChooseAttribute(this.ParameterType.GetCustomAttributes(false));
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00017322 File Offset: 0x00015522
		private static ParameterBindingAttribute ChooseAttribute(IList<ParameterBindingAttribute> list)
		{
			if (list.Count == 0)
			{
				return null;
			}
			if (list.Count > 1)
			{
				return new HttpParameterDescriptor.AmbiguousParameterBindingAttribute();
			}
			return list[0];
		}

		// Token: 0x040001FB RID: 507
		private readonly ConcurrentDictionary<object, object> _properties = new ConcurrentDictionary<object, object>();

		// Token: 0x040001FC RID: 508
		private ParameterBindingAttribute _parameterBindingAttribute;

		// Token: 0x040001FD RID: 509
		private bool _searchedModelBinderAttribute;

		// Token: 0x040001FE RID: 510
		private HttpConfiguration _configuration;

		// Token: 0x040001FF RID: 511
		private HttpActionDescriptor _actionDescriptor;

		// Token: 0x02000120 RID: 288
		private sealed class AmbiguousParameterBindingAttribute : ParameterBindingAttribute
		{
			// Token: 0x060006FD RID: 1789 RVA: 0x00017344 File Offset: 0x00015544
			public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameter)
			{
				string message = Error.Format(SRResources.ParameterBindingConflictingAttributes, new object[]
				{
					parameter.ParameterName
				});
				return parameter.BindAsError(message);
			}
		}
	}
}
