package uk.ac.nulondon;

import java.io.IOException;

public interface Command {
    //creating the generic execute function.
    void execute() throws IOException;
    void undo() throws IOException;
}
