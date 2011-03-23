using System;
using System.Linq;
using Machine.Specifications;
using Forge.Templating.SearchStrategies.Token;

namespace Forge.Templating.Specs.SearchStrategies.Token
{
    public class TemplateParserBase : SpecBase
    {
        internal static string template;
        internal static TemplateParser parser;
        internal static Tag tag;

        Because of = () => ex = Catch.Exception(() =>
                                                    {
                                                        parser = new TemplateParser(template.ToCharArray());
                                                        tag = parser.Process();
                                                    });

    }

    public class When_passed_a_null_template : SpecBase
    {
        Because of = () => ex = Catch.Exception(() => new TemplateParser(null));
        It should_throw_an_argument_exception = () => ex.ShouldBeOfType(typeof(ArgumentNullException));
    }

    public class When_passed_a_string_empty_template : TemplateParserBase
    {
        Establish context = () => template = string.Empty;
        It should_throw_an_argument_exception = () => ex.ShouldBeOfType(typeof(ArgumentOutOfRangeException));
    }

    public class When_passed_a_template_with_no_tags : TemplateParserBase
    {
        Establish context = () => template = "Test template without new lines";

        It should_return_a_tag_tree = () => tag.ShouldNotBeNull();
        It should_have_one_child_tag = () => tag.Children.Count.ShouldEqual(1);
        It should_be_a_content_tag = () => tag.Children.First().Type.ShouldEqual(TagRepository.TagTypes.Content);
    }

    public class When_passed_a_template_with_only_a_value_tag : TemplateParserBase
    {
        Establish context = () => template = "{person.name}";

        It should_return_a_tag_tree = () => tag.ShouldNotBeNull();
        It should_have_one_child_tag = () => tag.Children.Count.ShouldEqual(1);
        It should_be_a_value_tag = () => tag.Children.First().Type.ShouldEqual(TagRepository.TagTypes.Value);
    }

    public class When_passed_a_template_with_only_for_loop_tag : TemplateParserBase
    {
        Establish context = () => template = "{!foreach person in people}{!end}";

        It should_return_a_tag_tree = () => tag.ShouldNotBeNull();
        It should_have_one_child_tag = () => tag.Children.Count.ShouldEqual(1);
        It should_be_a_for_loop_tag = () => tag.Children.First().Type.ShouldEqual(TagRepository.TagTypes.ForLoop);
    }

    public class When_passed_a_template_with_a_value_tag_and_content : TemplateParserBase
    {
        Establish context = () => template = "{person.name}, welcome";

        It should_return_a_tag_tree = () => tag.ShouldNotBeNull();
        It should_have_two_child_tags = () => tag.Children.Count.ShouldEqual(2);
        It should_have_the_first_as_value_tag = () => tag.Children.First().Type.ShouldEqual(TagRepository.TagTypes.Value);
        It should_have_the_second_as_content_tag = () => tag.Children.Last().Type.ShouldEqual(TagRepository.TagTypes.Content);
    }

    public class When_passed_a_template_with_content_and_a_value_tag : TemplateParserBase
    {
        Establish context = () => template = "Welcome, {person.name}";

        It should_return_a_tag_tree = () => tag.ShouldNotBeNull();
        It should_have_two_child_tags = () => tag.Children.Count.ShouldEqual(2);
        It should_have_the_first_as_content_tag = () => tag.Children.First().Type.ShouldEqual(TagRepository.TagTypes.Content);
        It should_have_the_second_as_value_tag = () => tag.Children.Last().Type.ShouldEqual(TagRepository.TagTypes.Value);
    }
}