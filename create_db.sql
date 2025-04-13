CREATE TABLE table_books (
    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    title TEXT NOT NULL,
    author TEXT NOT NULL
);

INSERT INTO table_books (title, author) 
VALUES ('K19', 'Alexandr');