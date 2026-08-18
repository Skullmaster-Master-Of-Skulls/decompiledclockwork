using System;
using System.ComponentModel;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000175 RID: 373
	public class PropertyInterceptionContext<TValue> : DbInterceptionContext, IDbMutableInterceptionContext
	{
		// Token: 0x06000C4D RID: 3149 RVA: 0x0003A793 File Offset: 0x00038993
		public PropertyInterceptionContext()
		{
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x0003A7A8 File Offset: 0x000389A8
		public PropertyInterceptionContext(DbInterceptionContext copyFrom) : base(copyFrom)
		{
			Check.NotNull<DbInterceptionContext>(copyFrom, "copyFrom");
			PropertyInterceptionContext<TValue> propertyInterceptionContext = copyFrom as PropertyInterceptionContext<TValue>;
			if (propertyInterceptionContext != null)
			{
				this._value = propertyInterceptionContext._value;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000C4F RID: 3151 RVA: 0x0003A7E9 File Offset: 0x000389E9
		InterceptionContextMutableData IDbMutableInterceptionContext.MutableData
		{
			get
			{
				return this._mutableData;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000C50 RID: 3152 RVA: 0x0003A7F1 File Offset: 0x000389F1
		public TValue Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000C51 RID: 3153 RVA: 0x0003A7F9 File Offset: 0x000389F9
		// (set) Token: 0x06000C52 RID: 3154 RVA: 0x0003A806 File Offset: 0x00038A06
		public object UserState
		{
			get
			{
				return this._mutableData.UserState;
			}
			set
			{
				this._mutableData.UserState = value;
			}
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x0003A814 File Offset: 0x00038A14
		public PropertyInterceptionContext<TValue> WithValue(TValue value)
		{
			PropertyInterceptionContext<TValue> propertyInterceptionContext = this.TypedClone();
			propertyInterceptionContext._value = value;
			return propertyInterceptionContext;
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x0003A830 File Offset: 0x00038A30
		private PropertyInterceptionContext<TValue> TypedClone()
		{
			return (PropertyInterceptionContext<TValue>)this.Clone();
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x0003A83D File Offset: 0x00038A3D
		protected override DbInterceptionContext Clone()
		{
			return new PropertyInterceptionContext<TValue>(this);
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x0003A845 File Offset: 0x00038A45
		public bool IsExecutionSuppressed
		{
			get
			{
				return this._mutableData.IsExecutionSuppressed;
			}
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x0003A852 File Offset: 0x00038A52
		public void SuppressExecution()
		{
			this._mutableData.SuppressExecution();
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x0003A85F File Offset: 0x00038A5F
		public Exception OriginalException
		{
			get
			{
				return this._mutableData.OriginalException;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000C59 RID: 3161 RVA: 0x0003A86C File Offset: 0x00038A6C
		// (set) Token: 0x06000C5A RID: 3162 RVA: 0x0003A879 File Offset: 0x00038A79
		public Exception Exception
		{
			get
			{
				return this._mutableData.Exception;
			}
			set
			{
				this._mutableData.Exception = value;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000C5B RID: 3163 RVA: 0x0003A887 File Offset: 0x00038A87
		public TaskStatus TaskStatus
		{
			get
			{
				return this._mutableData.TaskStatus;
			}
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x0003A894 File Offset: 0x00038A94
		public new PropertyInterceptionContext<TValue> AsAsync()
		{
			return (PropertyInterceptionContext<TValue>)base.AsAsync();
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x0003A8A1 File Offset: 0x00038AA1
		public new PropertyInterceptionContext<TValue> WithDbContext(DbContext context)
		{
			Check.NotNull<DbContext>(context, "context");
			return (PropertyInterceptionContext<TValue>)base.WithDbContext(context);
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x0003A8BB File Offset: 0x00038ABB
		public new PropertyInterceptionContext<TValue> WithObjectContext(ObjectContext context)
		{
			Check.NotNull<ObjectContext>(context, "context");
			return (PropertyInterceptionContext<TValue>)base.WithObjectContext(context);
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x0003A8D5 File Offset: 0x00038AD5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x0003A8DD File Offset: 0x00038ADD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x0003A8E6 File Offset: 0x00038AE6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x0003A8EE File Offset: 0x00038AEE
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000344 RID: 836
		private readonly InterceptionContextMutableData _mutableData = new InterceptionContextMutableData();

		// Token: 0x04000345 RID: 837
		private TValue _value;
	}
}
