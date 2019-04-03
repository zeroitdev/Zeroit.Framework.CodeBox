// ***********************************************************************
// Assembly         : Zeroit.Framework.CodeBox
// Author           : ZEROIT
// Created          : 03-19-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-05-2019
// ***********************************************************************
// <copyright file="Lexer.cs" company="Zeroit Dev">
//    This program is for creating a Code Editor control.
//    Copyright ©  2017  Zeroit Dev Technologies
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <https://www.gnu.org/licenses/>.
//
//    You can contact me at zeroitdevnet@gmail.com or zeroitdev@outlook.com
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Zeroit.Framework.CodeBox
{
    /// <summary>
    /// Specifies the lexer to use for syntax highlighting in a <see cref="Scintilla" /> control.
    /// </summary>
    public enum Lexer
    {
        /// <summary>
        /// Lexing is performed by the <see cref="Scintilla" /> control container (host) using
        /// the <see cref="Scintilla.StyleNeeded" /> event.
        /// </summary>
        Container = NativeMethods.SCLEX_CONTAINER,

        /// <summary>
        /// No lexing should be performed.
        /// </summary>
        Null = NativeMethods.SCLEX_NULL,

        /// <summary>
        /// The Ada (95) language lexer.
        /// </summary>
        Ada = NativeMethods.SCLEX_ADA,

        /// <summary>
        /// The assembly language lexer.
        /// </summary>
        Asm = NativeMethods.SCLEX_ASM,

        /// <summary>
        /// The batch file lexer.
        /// </summary>
        Batch = NativeMethods.SCLEX_BATCH,

        /// <summary>
        /// The C language family (C++, C, C#, Java, JavaScript, etc...) lexer.
        /// </summary>
        Cpp = NativeMethods.SCLEX_CPP,

        /// <summary>
        /// The Cascading Style Sheets (CSS, SCSS) lexer.
        /// </summary>
        Css = NativeMethods.SCLEX_CSS,

        /// <summary>
        /// The Fortran language lexer.
        /// </summary>
        Fortran = NativeMethods.SCLEX_FORTRAN,

        /// <summary>
        /// The FreeBASIC language lexer.
        /// </summary>
        FreeBasic = NativeMethods.SCLEX_FREEBASIC,

        /// <summary>
        /// The HyperText Markup Language (HTML) lexer.
        /// </summary>
        Html = NativeMethods.SCLEX_HTML,

        /// <summary>
        /// JavaScript Object Notation (JSON) lexer.
        /// </summary>
        Json = NativeMethods.SCLEX_JSON,

        /// <summary>
        /// The Lisp language lexer.
        /// </summary>
        Lisp = NativeMethods.SCLEX_LISP,

        /// <summary>
        /// The Lua scripting language lexer.
        /// </summary>
        Lua = NativeMethods.SCLEX_LUA,

        /// <summary>
        /// The Matlab scripting language lexer.
        /// </summary>
        Matlab = NativeMethods.SCLEX_MATLAB,

        /// <summary>
        /// The Pascal language lexer.
        /// </summary>
        Pascal = NativeMethods.SCLEX_PASCAL,

        /// <summary>
        /// The Perl language lexer.
        /// </summary>
        Perl = NativeMethods.SCLEX_PERL,

        /// <summary>
        /// The PHP: Hypertext Preprocessor (PHP) script lexer.
        /// </summary>
        PhpScript = NativeMethods.SCLEX_PHPSCRIPT,

        /// <summary>
        /// PowerShell script lexer.
        /// </summary>
        PowerShell = NativeMethods.SCLEX_POWERSHELL,

        /// <summary>
        /// Properties file (INI) lexer.
        /// </summary>
        Properties = NativeMethods.SCLEX_PROPERTIES,

        /// <summary>
        /// The PureBasic language lexer.
        /// </summary>
        PureBasic = NativeMethods.SCLEX_PUREBASIC,

        /// <summary>
        /// The Python language lexer.
        /// </summary>
        Python = NativeMethods.SCLEX_PYTHON,

        /// <summary>
        /// The Ruby language lexer.
        /// </summary>
        Ruby = NativeMethods.SCLEX_RUBY,

        /// <summary>
        /// The SmallTalk language lexer.
        /// </summary>
        Smalltalk = NativeMethods.SCLEX_SMALLTALK,

        /// <summary>
        /// The Structured Query Language (SQL) lexer.
        /// </summary>
        Sql = NativeMethods.SCLEX_SQL,

        /// <summary>
        /// The Visual Basic (VB) lexer.
        /// </summary>
        Vb = NativeMethods.SCLEX_VB,

        /// <summary>
        /// The Visual Basic Script (VBScript) lexer.
        /// </summary>
        VbScript = NativeMethods.SCLEX_VBSCRIPT,

        /// <summary>
        /// The Verilog hardware description language lexer.
        /// </summary>
        Verilog = NativeMethods.SCLEX_VERILOG,

        /// <summary>
        /// The Extensible Markup Language (XML) lexer.
        /// </summary>
        Xml = NativeMethods.SCLEX_XML,

        /// <summary>
        /// The Blitz (Blitz3D, BlitzMax, etc...) variant of Basic lexer.
        /// </summary>
        BlitzBasic = NativeMethods.SCLEX_BLITZBASIC,

        /// <summary>
        /// The Markdown syntax lexer.
        /// </summary>
        Markdown = NativeMethods.SCLEX_MARKDOWN,

        /// <summary>
        /// The R programming language lexer.
        /// </summary>
        R = NativeMethods.SCLEX_R
    }
}
