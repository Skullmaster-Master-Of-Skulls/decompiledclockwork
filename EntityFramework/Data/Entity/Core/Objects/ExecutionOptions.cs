using System;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x020001FF RID: 511
	public class ExecutionOptions
	{
		// Token: 0x06001238 RID: 4664 RVA: 0x0004CCAD File Offset: 0x0004AEAD
		public ExecutionOptions(MergeOption mergeOption)
		{
			this.MergeOption = mergeOption;
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x0004CCBC File Offset: 0x0004AEBC
		public ExecutionOptions(MergeOption mergeOption, bool streaming)
		{
			this.MergeOption = mergeOption;
			this.UserSpecifiedStreaming = new bool?(streaming);
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x0004CCD7 File Offset: 0x0004AED7
		internal ExecutionOptions(MergeOption mergeOption, bool? streaming)
		{
			this.MergeOption = mergeOption;
			this.UserSpecifiedStreaming = streaming;
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600123B RID: 4667 RVA: 0x0004CCED File Offset: 0x0004AEED
		// (set) Token: 0x0600123C RID: 4668 RVA: 0x0004CCF5 File Offset: 0x0004AEF5
		public MergeOption MergeOption { get; private set; }

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600123D RID: 4669 RVA: 0x0004CD00 File Offset: 0x0004AF00
		[Obsolete("Queries are now streaming by default unless a retrying ExecutionStrategy is used. This property no longer returns an accurate value.")]
		public bool Streaming
		{
			get
			{
				return this.UserSpecifiedStreaming ?? true;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x0600123E RID: 4670 RVA: 0x0004CD26 File Offset: 0x0004AF26
		// (set) Token: 0x0600123F RID: 4671 RVA: 0x0004CD2E File Offset: 0x0004AF2E
		internal bool? UserSpecifiedStreaming { get; private set; }

		// Token: 0x06001240 RID: 4672 RVA: 0x0004CD37 File Offset: 0x0004AF37
		public static bool operator ==(ExecutionOptions left, ExecutionOptions right)
		{
			return object.ReferenceEquals(left, right) || (!object.ReferenceEquals(left, null) && left.Equals(right));
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x0004CD56 File Offset: 0x0004AF56
		public static bool operator !=(ExecutionOptions left, ExecutionOptions right)
		{
			return !(left == right);
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x0004CD64 File Offset: 0x0004AF64
		public override bool Equals(object obj)
		{
			ExecutionOptions executionOptions = obj as ExecutionOptions;
			return !object.ReferenceEquals(executionOptions, null) && this.MergeOption == executionOptions.MergeOption && this.UserSpecifiedStreaming == executionOptions.UserSpecifiedStreaming;
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x0004CDC4 File Offset: 0x0004AFC4
		public override int GetHashCode()
		{
			return this.MergeOption.GetHashCode() ^ this.UserSpecifiedStreaming.GetHashCode();
		}

		// Token: 0x04000559 RID: 1369
		internal static readonly ExecutionOptions Default = new ExecutionOptions(MergeOption.AppendOnly);
	}
}
