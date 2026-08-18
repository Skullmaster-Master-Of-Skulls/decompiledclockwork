using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000014 RID: 20
	public class ParametersWithRandom : ICipherParameters
	{
		// Token: 0x06000083 RID: 131 RVA: 0x0000580C File Offset: 0x0000480C
		public ParametersWithRandom(ICipherParameters parameters, SecureRandom random)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("random");
			}
			if (random == null)
			{
				throw new ArgumentNullException("random");
			}
			this.parameters = parameters;
			this.random = random;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000583E File Offset: 0x0000483E
		public ParametersWithRandom(ICipherParameters parameters) : this(parameters, new SecureRandom())
		{
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000584C File Offset: 0x0000484C
		[Obsolete("Use Random property instead")]
		public SecureRandom GetRandom()
		{
			return this.Random;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00005854 File Offset: 0x00004854
		public SecureRandom Random
		{
			get
			{
				return this.random;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000087 RID: 135 RVA: 0x0000585C File Offset: 0x0000485C
		public ICipherParameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x04000043 RID: 67
		private readonly ICipherParameters parameters;

		// Token: 0x04000044 RID: 68
		private readonly SecureRandom random;
	}
}
