using System.Windows.Forms;
using Microsoft.Win32;

namespace EasyNoteNS
{
  public static class RegControls
  {
    private static RegistryKey autorun = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
    private static RegistryKey key = null;

    public static bool Init()
    {
      autorun.SetValue("EasyNote", @Application.ExecutablePath);

      key = Registry.CurrentUser.OpenSubKey(@"Software\EasyNote", true);

      if (key == null)
      {
        key = Registry.CurrentUser.CreateSubKey(@"Software\EasyNote");
        return false;
      }

      return true;
    }

    public static dynamic Get(string name)
    {
      return key.GetValue(name);
    }

    public static void Set(string name, dynamic value)
    {
      key.SetValue(name, value);
    }
  }
}
