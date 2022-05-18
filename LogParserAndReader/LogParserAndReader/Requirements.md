# Requirements
## Use Cases
* As a `Software Developer` I must be able to open and view a log file in the reader.
* As a `Software Developer` I must be able to filter entries in an open log file.
* As a `Software Developer` I must be able to configure the application to recognize an arbitrary log file patterns.
* As a `Software Developer` I must be able to get file change updates without reopening the log file. 

## Acceptance Criteria
* **WHEN** reading in a logfile **THEN** display all log entries in a list
* **GIVEN** a configuration pattern **WHEN** reading the logfile **THEN** break the log file into the components outlined in the configuration pattern
* **GIVEN** a parsed log file, broken down by the pattern **WHEN** read into the application **THEN** allow filtering on each component of the log file
* **GIVEN** an open log file **WHEN** the file is updated outside of the reader **THEN** reload the log file into the log reader application

## Constraints
* The log files that are read and interpreted will adhere to a single template string
