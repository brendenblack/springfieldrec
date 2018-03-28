using HtmlTags;
using HtmlTags.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpringfieldRecMvc.Infrastructure.Tags
{
    public class TagConventions : HtmlConventionRegistry
    {
        public TagConventions()
        {

            Editors.Always.AddClass("form-control");

            Editors.IfPropertyIs<DateTime?>().ModifyWith(m => m.CurrentTag
                .AddPattern("9{1,2}/9{1,2}/9999")
                .AddPlaceholder("MM/DD/YYYY")
                .AddClass("datepicker")
                .Value(m.Value<DateTime?>() != null ? m.Value<DateTime>().ToShortDateString() : string.Empty));

            Editors.IfPropertyHasAttribute<EmailAddressAttribute>().Attr("placeholder", "email@example.com");
            Editors.IfPropertyHasAttribute<PhoneAttribute>().Attr("placeholder", "1-250-555-1234");

            Editors.If(er => er.Accessor.Name.EndsWith("id", StringComparison.OrdinalIgnoreCase)).BuildBy(a => new HiddenTag().Value(a.StringValue()));
            Editors.IfPropertyIs<byte[]>().BuildBy(a => new HiddenTag().Value(Convert.ToBase64String(a.Value<byte[]>())));


            Labels.Always.AddClass("control-label");
            Labels.ModifyForAttribute<DisplayAttribute>((t, a) => t.Text(a.Name));

            DisplayLabels.Always.BuildBy<DefaultDisplayLabelBuilder>();

            DisplayLabels.ModifyForAttribute<DisplayAttribute>((t, a) => t.Text(a.Name));
            Displays.IfPropertyIs<DateTime>().ModifyWith(m => m.CurrentTag.Text(m.Value<DateTime>().ToShortDateString()));
            Displays.IfPropertyIs<DateTime?>().ModifyWith(m => m.CurrentTag.Text(m.Value<DateTime?>() == null ? null : m.Value<DateTime?>().Value.ToShortDateString()));
            Displays.IfPropertyIs<decimal>().ModifyWith(m => m.CurrentTag.Text(m.Value<decimal>().ToString("C")));

            this.Defaults();
        }

        public ElementCategoryExpression DisplayLabels => new ElementCategoryExpression(Library.TagLibrary.Category("DisplayLabels").Profile(TagConstants.Default));
    }
}
