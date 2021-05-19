using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace Qulaly.Matcher
{
    internal class EnumerableMatcher
    {
        public static IEnumerable<SyntaxNode> GetEnumerable(SyntaxNode node, QulalySelector selector, SemanticModel? semanticModel)
        {
            if (selector.Matcher(new SelectorMatcherContext(node, semanticModel)))
            {
                yield return node;
            }

            foreach (var child in node.ChildNodes())
            {
                foreach (var next in GetEnumerable(child, selector, semanticModel))
                {
                    yield return next;
                }
            }
        }

        public static IEnumerable<SyntaxNode[]> GetCapturesEnumerable(SyntaxNode node, QulalySelector selector, SemanticModel? semanticModel)
        {
            var ctx = new SelectorMatcherContext(node, semanticModel);
            if (selector.Matcher(ctx))
            {
                ctx.CapturedNodes.Reverse();
                yield return ctx.CapturedNodes.ToArray();
            }

            foreach (var child in node.ChildNodes())
            {
                foreach (var next in GetCapturesEnumerable(child, selector, semanticModel))
                {
                    yield return next;
                }
            }
        }
    }
}
