using System.Threading.Tasks;

namespace Bmbsqd.SaferPay
{
	public interface ISaferPayDispatcher
	{
		Task<ISaferPayResponse> DispatchAsync( ISaferPayRequest request );
	}
}