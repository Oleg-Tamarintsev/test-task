grammar PathGrammar;

/*
 * Parser Rules
 */

path : (absolutePath | relativePath) EOF;

absolutePath : rootPath (pathSegment (PATH_DELIMITER pathSegment)*)?;

relativePath : pathSegment (PATH_DELIMITER pathSegment)*;

directory : DIRECTORY;

parentDirecoty : PARENT_DIRECTORY;

rootPath : PATH_DELIMITER;

pathSegment: directory | parentDirecoty;

/*
 * Lexer Rules
 */

DIRECTORY: ([a-z] | [A-Z])+;
PARENT_DIRECTORY : '..';
PATH_DELIMITER : '/';