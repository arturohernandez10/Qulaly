using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace Qulaly.Matcher
{
    public readonly struct SelectorMatcherContext
    {
        public SyntaxNode Node { get; }
        public SemanticModel? SemanticModel { get; }

        public List<SyntaxNode> CapturedNodes { get; }

        public SelectorMatcherContext(SyntaxNode node, SemanticModel? semanticModel, List<SyntaxNode>? capturedNodes = null)
        {
            Node = node ?? throw new ArgumentNullException(nameof(node));
            SemanticModel = semanticModel;
            CapturedNodes = capturedNodes ?? new List<SyntaxNode>();
        }

        public SelectorMatcherContext WithSyntaxNode(SyntaxNode node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            return new SelectorMatcherContext(node, SemanticModel, CapturedNodes);
        }
    }
}
