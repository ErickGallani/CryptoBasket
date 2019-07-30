#!/bin/bash
REGEX_PATTERN="<Linecoverage>(\d*)%<\/Linecoverage>"

ls

ls ..

TEST_RESULTS=$(cat ../reports/Summary.xml)

cat $TEST_RESULTS

if [[ $TEST_RESULTS =~ REGEX_PATTERN ]]; then
    
    echo BASH_REMATCH
    echo BASH_REMATCH[1]

    if [[ BASH_REMATCH[1] -lt 80 ]];  then
        echo "Code coverage less than 80%"
        exit 1
    fi
fi