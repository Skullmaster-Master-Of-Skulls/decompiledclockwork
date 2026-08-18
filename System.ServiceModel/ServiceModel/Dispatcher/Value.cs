using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200050B RID: 1291
	internal struct Value
	{
		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x060030CC RID: 12492 RVA: 0x000BAFDF File Offset: 0x000B91DF
		// (set) Token: 0x060030CD RID: 12493 RVA: 0x000BAFE7 File Offset: 0x000B91E7
		internal bool Boolean
		{
			get
			{
				return this.boolVal;
			}
			set
			{
				this.type = ValueDataType.Boolean;
				this.boolVal = value;
			}
		}

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x060030CE RID: 12494 RVA: 0x000BAFF7 File Offset: 0x000B91F7
		// (set) Token: 0x060030CF RID: 12495 RVA: 0x000BAFFF File Offset: 0x000B91FF
		internal double Double
		{
			get
			{
				return this.dblVal;
			}
			set
			{
				this.type = ValueDataType.Double;
				this.dblVal = value;
			}
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x060030D0 RID: 12496 RVA: 0x000BB00F File Offset: 0x000B920F
		internal StackFrame Frame
		{
			get
			{
				return this.frame;
			}
		}

		// Token: 0x17000B8E RID: 2958
		// (set) Token: 0x060030D1 RID: 12497 RVA: 0x000BB017 File Offset: 0x000B9217
		internal int FrameEndPtr
		{
			set
			{
				this.frame.EndPtr = value;
			}
		}

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x060030D2 RID: 12498 RVA: 0x000BB025 File Offset: 0x000B9225
		internal int NodeCount
		{
			get
			{
				return this.sequence.Count;
			}
		}

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x060030D3 RID: 12499 RVA: 0x000BB032 File Offset: 0x000B9232
		// (set) Token: 0x060030D4 RID: 12500 RVA: 0x000BB03A File Offset: 0x000B923A
		internal NodeSequence Sequence
		{
			get
			{
				return this.sequence;
			}
			set
			{
				this.type = ValueDataType.Sequence;
				this.sequence = value;
			}
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x060030D5 RID: 12501 RVA: 0x000BB04A File Offset: 0x000B924A
		// (set) Token: 0x060030D6 RID: 12502 RVA: 0x000BB052 File Offset: 0x000B9252
		internal string String
		{
			get
			{
				return this.strVal;
			}
			set
			{
				this.type = ValueDataType.String;
				this.strVal = value;
			}
		}

		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x060030D7 RID: 12503 RVA: 0x000BB062 File Offset: 0x000B9262
		internal ValueDataType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x060030D8 RID: 12504 RVA: 0x000BB06A File Offset: 0x000B926A
		internal void Add(double val)
		{
			this.dblVal += val;
		}

		// Token: 0x060030D9 RID: 12505 RVA: 0x000BB07A File Offset: 0x000B927A
		internal void Clear(ProcessingContext context)
		{
			if (ValueDataType.Sequence == this.type)
			{
				this.ReleaseSequence(context);
			}
			this.type = ValueDataType.None;
		}

		// Token: 0x060030DA RID: 12506 RVA: 0x000BB094 File Offset: 0x000B9294
		internal bool CompareTo(ref Value val, RelationOperator op)
		{
			switch (this.type)
			{
			case ValueDataType.Boolean:
				switch (val.type)
				{
				case ValueDataType.Boolean:
					return QueryValueModel.Compare(this.boolVal, val.boolVal, op);
				case ValueDataType.Double:
					return QueryValueModel.Compare(this.boolVal, val.dblVal, op);
				default:
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new QueryProcessingException(QueryProcessingError.TypeMismatch));
				case ValueDataType.Sequence:
					return QueryValueModel.Compare(this.boolVal, val.sequence, op);
				case ValueDataType.String:
					return QueryValueModel.Compare(this.boolVal, val.strVal, op);
				}
				break;
			case ValueDataType.Double:
				switch (val.type)
				{
				case ValueDataType.Boolean:
					return QueryValueModel.Compare(this.dblVal, val.boolVal, op);
				case ValueDataType.Double:
					return QueryValueModel.Compare(this.dblVal, val.dblVal, op);
				default:
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new QueryProcessingException(QueryProcessingError.TypeMismatch));
				case ValueDataType.Sequence:
					return QueryValueModel.Compare(this.dblVal, val.sequence, op);
				case ValueDataType.String:
					return QueryValueModel.Compare(this.dblVal, val.strVal, op);
				}
				break;
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new QueryProcessingException(QueryProcessingError.TypeMismatch));
			case ValueDataType.Sequence:
				switch (val.type)
				{
				case ValueDataType.Boolean:
					return QueryValueModel.Compare(this.sequence, val.boolVal, op);
				case ValueDataType.Double:
					return QueryValueModel.Compare(this.sequence, val.dblVal, op);
				default:
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new QueryProcessingException(QueryProcessingError.TypeMismatch));
				case ValueDataType.Sequence:
					return QueryValueModel.Compare(this.sequence, val.sequence, op);
				case ValueDataType.String:
					return QueryValueModel.Compare(this.sequence, val.strVal, op);
				}
				break;
			case ValueDataType.String:
				switch (val.type)
				{
				case ValueDataType.Boolean:
					return QueryValueModel.Compare(this.strVal, val.boolVal, op);
				case ValueDataType.Double:
					return QueryValueModel.Compare(this.strVal, val.dblVal, op);
				default:
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new QueryProcessingException(QueryProcessingError.TypeMismatch));
				case ValueDataType.Sequence:
					return QueryValueModel.Compare(this.strVal, val.sequence, op);
				case ValueDataType.String:
					return QueryValueModel.Compare(this.strVal, val.strVal, op);
				}
				break;
			}
		}

		// Token: 0x060030DB RID: 12507 RVA: 0x000BB2D8 File Offset: 0x000B94D8
		internal bool CompareTo(double val, RelationOperator op)
		{
			switch (this.type)
			{
			case ValueDataType.Boolean:
				return QueryValueModel.Compare(this.boolVal, val, op);
			case ValueDataType.Double:
				return QueryValueModel.Compare(this.dblVal, val, op);
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new QueryProcessingException(QueryProcessingError.TypeMismatch));
			case ValueDataType.Sequence:
				return QueryValueModel.Compare(this.sequence, val, op);
			case ValueDataType.String:
				return QueryValueModel.Compare(this.strVal, val, op);
			}
		}

		// Token: 0x060030DC RID: 12508 RVA: 0x000BB350 File Offset: 0x000B9550
		internal void ConvertTo(ProcessingContext context, ValueDataType newType)
		{
			if (newType == this.type)
			{
				return;
			}
			switch (newType)
			{
			case ValueDataType.Boolean:
				this.boolVal = this.ToBoolean();
				break;
			case ValueDataType.Double:
				this.dblVal = this.ToDouble();
				break;
			case ValueDataType.String:
				this.strVal = this.ToString();
				break;
			}
			if (ValueDataType.Sequence == this.type)
			{
				this.ReleaseSequence(context);
			}
			this.type = newType;
		}

		// Token: 0x060030DD RID: 12509 RVA: 0x000BB3CC File Offset: 0x000B95CC
		internal bool Equals(string val)
		{
			switch (this.type)
			{
			case ValueDataType.Boolean:
				return QueryValueModel.Equals(this.boolVal, val);
			case ValueDataType.Double:
				return QueryValueModel.Equals(this.dblVal, val);
			default:
				return false;
			case ValueDataType.Sequence:
				return QueryValueModel.Equals(this.sequence, val);
			case ValueDataType.String:
				return QueryValueModel.Equals(this.strVal, val);
			}
		}

		// Token: 0x060030DE RID: 12510 RVA: 0x000BB434 File Offset: 0x000B9634
		internal bool Equals(double val)
		{
			switch (this.type)
			{
			case ValueDataType.Boolean:
				return QueryValueModel.Equals(this.boolVal, val);
			case ValueDataType.Double:
				return QueryValueModel.Equals(this.dblVal, val);
			default:
				return false;
			case ValueDataType.Sequence:
				return QueryValueModel.Equals(this.sequence, val);
			case ValueDataType.String:
				return QueryValueModel.Equals(val, this.strVal);
			}
		}

		// Token: 0x060030DF RID: 12511 RVA: 0x000BB499 File Offset: 0x000B9699
		internal bool GetBoolean()
		{
			if (ValueDataType.Boolean != this.type)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new QueryProcessingException(QueryProcessingError.TypeMismatch));
			}
			return this.boolVal;
		}

		// Token: 0x060030E0 RID: 12512 RVA: 0x000BB4BB File Offset: 0x000B96BB
		internal double GetDouble()
		{
			if (ValueDataType.Double != this.type)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new QueryProcessingException(QueryProcessingError.TypeMismatch));
			}
			return this.dblVal;
		}

		// Token: 0x060030E1 RID: 12513 RVA: 0x000BB4DD File Offset: 0x000B96DD
		internal NodeSequence GetSequence()
		{
			if (ValueDataType.Sequence != this.type)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new QueryProcessingException(QueryProcessingError.TypeMismatch));
			}
			return this.sequence;
		}

		// Token: 0x060030E2 RID: 12514 RVA: 0x000BB4FF File Offset: 0x000B96FF
		internal string GetString()
		{
			if (ValueDataType.String != this.type)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new QueryProcessingException(QueryProcessingError.TypeMismatch));
			}
			return this.strVal;
		}

		// Token: 0x060030E3 RID: 12515 RVA: 0x000BB521 File Offset: 0x000B9721
		internal bool IsType(ValueDataType type)
		{
			return type == this.type;
		}

		// Token: 0x060030E4 RID: 12516 RVA: 0x000BB52C File Offset: 0x000B972C
		internal void Multiply(double val)
		{
			this.dblVal *= val;
		}

		// Token: 0x060030E5 RID: 12517 RVA: 0x000BB53C File Offset: 0x000B973C
		internal void Negate()
		{
			this.dblVal = -this.dblVal;
		}

		// Token: 0x060030E6 RID: 12518 RVA: 0x000BB54B File Offset: 0x000B974B
		internal void Not()
		{
			this.boolVal = !this.boolVal;
		}

		// Token: 0x060030E7 RID: 12519 RVA: 0x000BB55C File Offset: 0x000B975C
		internal void ReleaseSequence(ProcessingContext context)
		{
			context.ReleaseSequence(this.sequence);
			this.sequence = null;
		}

		// Token: 0x060030E8 RID: 12520 RVA: 0x000BB571 File Offset: 0x000B9771
		internal void StartFrame(int start)
		{
			this.type = ValueDataType.StackFrame;
			this.frame.basePtr = start + 1;
			this.frame.endPtr = start;
		}

		// Token: 0x060030E9 RID: 12521 RVA: 0x000BB594 File Offset: 0x000B9794
		internal bool ToBoolean()
		{
			switch (this.type)
			{
			case ValueDataType.Boolean:
				return this.boolVal;
			case ValueDataType.Double:
				return QueryValueModel.Boolean(this.dblVal);
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new QueryProcessingException(QueryProcessingError.TypeMismatch));
			case ValueDataType.Sequence:
				return QueryValueModel.Boolean(this.sequence);
			case ValueDataType.String:
				return QueryValueModel.Boolean(this.strVal);
			}
		}

		// Token: 0x060030EA RID: 12522 RVA: 0x000BB600 File Offset: 0x000B9800
		internal double ToDouble()
		{
			switch (this.type)
			{
			case ValueDataType.Boolean:
				return QueryValueModel.Double(this.boolVal);
			case ValueDataType.Double:
				return this.dblVal;
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new QueryProcessingException(QueryProcessingError.TypeMismatch));
			case ValueDataType.Sequence:
				return QueryValueModel.Double(this.sequence);
			case ValueDataType.String:
				return QueryValueModel.Double(this.strVal);
			}
		}

		// Token: 0x060030EB RID: 12523 RVA: 0x000BB66C File Offset: 0x000B986C
		public override string ToString()
		{
			switch (this.type)
			{
			case ValueDataType.Boolean:
				return QueryValueModel.String(this.boolVal);
			case ValueDataType.Double:
				return QueryValueModel.String(this.dblVal);
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new QueryProcessingException(QueryProcessingError.TypeMismatch));
			case ValueDataType.Sequence:
				return QueryValueModel.String(this.sequence);
			case ValueDataType.String:
				return this.strVal;
			}
		}

		// Token: 0x060030EC RID: 12524 RVA: 0x000BB6D7 File Offset: 0x000B98D7
		internal void Update(ProcessingContext context, bool val)
		{
			if (ValueDataType.Sequence == this.type)
			{
				context.ReleaseSequence(this.sequence);
			}
			this.Boolean = val;
		}

		// Token: 0x060030ED RID: 12525 RVA: 0x000BB6F5 File Offset: 0x000B98F5
		internal void Update(ProcessingContext context, double val)
		{
			if (ValueDataType.Sequence == this.type)
			{
				context.ReleaseSequence(this.sequence);
			}
			this.Double = val;
		}

		// Token: 0x060030EE RID: 12526 RVA: 0x000BB713 File Offset: 0x000B9913
		internal void Update(ProcessingContext context, string val)
		{
			if (ValueDataType.Sequence == this.type)
			{
				context.ReleaseSequence(this.sequence);
			}
			this.String = val;
		}

		// Token: 0x060030EF RID: 12527 RVA: 0x000BB731 File Offset: 0x000B9931
		internal void Update(ProcessingContext context, NodeSequence val)
		{
			if (ValueDataType.Sequence == this.type)
			{
				context.ReleaseSequence(this.sequence);
			}
			this.Sequence = val;
		}

		// Token: 0x0400261D RID: 9757
		private bool boolVal;

		// Token: 0x0400261E RID: 9758
		private double dblVal;

		// Token: 0x0400261F RID: 9759
		private StackFrame frame;

		// Token: 0x04002620 RID: 9760
		private NodeSequence sequence;

		// Token: 0x04002621 RID: 9761
		private string strVal;

		// Token: 0x04002622 RID: 9762
		private ValueDataType type;
	}
}
