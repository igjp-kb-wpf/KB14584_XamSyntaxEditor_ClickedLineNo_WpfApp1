using Infragistics.Controls.Editors;
using Infragistics.Controls.Editors.Primitives;
using Infragistics.Windows;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KB14584_XamSyntaxEditor_ClickedLineNo_WpfApp1;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void xamSyntaxEditor1_Loaded(object sender, RoutedEventArgs e)
    {
        for (int i = 0; i < 10; i++)
        {
            textDocument.Append("Hello World!" + Environment.NewLine);
        }
    }

    private void LineNumberMarginPresenter_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        // DocumentViewのテキスト描画領域EditorDocumentViewTextAreaを取得します。
        // プロパティで公開されていないので、Visual Treeをたどる必要があります。
        // そのため、いったんLineNumberPresenterから親へと辿ってXamSyntaxEditorを取得し、
        // その子要素の中からEditorDocumentViewTextAreaを取得しています。
        var lineNumberMarginPresenter = (LineNumberMarginPresenter)sender;
        var xamSyntaxEditor = Utilities.GetAncestorFromType(lineNumberMarginPresenter, typeof(XamSyntaxEditor), false) as XamSyntaxEditor;
        if (xamSyntaxEditor == null) return;
        var textArea = Utilities.GetDescendantFromType(xamSyntaxEditor, typeof(EditorDocumentViewTextArea), false) as EditorDocumentViewTextArea;
        if (textArea == null) return;

        var mouseDownPointInTextAreaCoordinates = e.GetPosition(textArea);
        var line = xamSyntaxEditor.ActiveDocumentView.ViewLineFromVerticalOffset(mouseDownPointInTextAreaCoordinates.Y);
        if (line != null)
        {
            textBlock1.Text = $"{line.SnapshotLineInfo.LineNumber.ToString()}行目がクリックされました！";
        }
    }
}