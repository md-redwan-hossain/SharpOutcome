#if NET11_0_OR_GREATER
using SharpOutcome.Helpers;

namespace SharpOutcome
{
    public union HttpUnionOutcome(HttpGoodOutcome, HttpBadOutcome);

    public union HttpUnionOutcome<TGood>(HttpGoodOutcome<TGood>, HttpBadOutcome);

    public union HttpUnionOutcome<TGood, TBad>(HttpGoodOutcome<TGood>, HttpBadOutcome<TBad>);
}
#endif