using System;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Spatial
{
	// Token: 0x0200071D RID: 1821
	public abstract class DbSpatialDataReader
	{
		// Token: 0x06004A0B RID: 18955
		public abstract DbGeography GetGeography(int ordinal);

		// Token: 0x06004A0C RID: 18956 RVA: 0x00160420 File Offset: 0x0015E620
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Exception provided in the returned task.")]
		public virtual Task<DbGeography> GetGeographyAsync(int ordinal, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return TaskHelper.FromCancellation<DbGeography>();
			}
			Task<DbGeography> result;
			try
			{
				result = Task.FromResult<DbGeography>(this.GetGeography(ordinal));
			}
			catch (Exception ex)
			{
				result = TaskHelper.FromException<DbGeography>(ex);
			}
			return result;
		}

		// Token: 0x06004A0D RID: 18957
		public abstract DbGeometry GetGeometry(int ordinal);

		// Token: 0x06004A0E RID: 18958 RVA: 0x00160468 File Offset: 0x0015E668
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Exception provided in the returned task.")]
		public virtual Task<DbGeometry> GetGeometryAsync(int ordinal, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return TaskHelper.FromCancellation<DbGeometry>();
			}
			Task<DbGeometry> result;
			try
			{
				result = Task.FromResult<DbGeometry>(this.GetGeometry(ordinal));
			}
			catch (Exception ex)
			{
				result = TaskHelper.FromException<DbGeometry>(ex);
			}
			return result;
		}

		// Token: 0x06004A0F RID: 18959
		public abstract bool IsGeographyColumn(int ordinal);

		// Token: 0x06004A10 RID: 18960
		public abstract bool IsGeometryColumn(int ordinal);
	}
}
