using Microsoft.CodeAnalysis.CSharp;

namespace Qulaly.Matcher.Selectors
{
    public class CaptureSelector : Selector
    {
        public Selector selector { get; }

        public CaptureSelector(Selector typeSelector)
        {
            selector = typeSelector;
        }

        // Type Selector
        public override SelectorMatcher GetMatcher()
        {
            return (in SelectorMatcherContext ctx) =>
            {
                var success = selector.GetMatcher()(ctx);
                if (success)
                    ctx.CapturedNodes.Add(ctx.Node);
                return success;
            };
        }

        public override string ToSelectorString()
        {
            return "$" + selector.ToSelectorString();
        }
    }
    public class TypeSelector : Selector
    {
        public SyntaxKind Kind { get; }

        public TypeSelector(SyntaxKind kind)
        {
            Kind = kind;
        }

        // Type Selector
        public override SelectorMatcher GetMatcher()
        {
            return (in SelectorMatcherContext ctx) =>
            {
                return ctx.Node.Kind() == Kind;
            };
        }

        public override string ToSelectorString()
        {
            return Kind.ToString();
        }
    }

    public class UniversalTypeSelector : Selector
    {

        public UniversalTypeSelector()
        {
        }

        // Type Selector
        public override SelectorMatcher GetMatcher()
        {
            // Wildcard selector
            return (in SelectorMatcherContext ctx) => true;
        }

        public override string ToSelectorString()
        {
            return "*";
        }
    }
}
