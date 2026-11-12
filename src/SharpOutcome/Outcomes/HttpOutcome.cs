#if NET11_0_OR_GREATER
using SharpOutcome.Helpers;

namespace SharpOutcome
{
    public union HttpOutcome(HttpGoodOutcome, HttpBadOutcome);

    public union HttpOutcome<TGood>(HttpGoodOutcome<TGood>, HttpBadOutcome);

    public union HttpOutcome<TGood, TBad>(HttpGoodOutcome<TGood>, HttpBadOutcome<TBad>);
}
#endif