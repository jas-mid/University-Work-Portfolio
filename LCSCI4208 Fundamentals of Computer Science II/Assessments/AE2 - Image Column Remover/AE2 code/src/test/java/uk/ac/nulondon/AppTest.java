// ===== STARTING CODE (provided by the module) =====
package uk.ac.nulondon;

import org.approvaltests.Approvals;
import org.approvaltests.awt.AwtApprovals;
import org.assertj.core.api.Assertions;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import javax.imageio.ImageIO;
import java.awt.Color;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;
import java.util.List;

public class AppTest {

    Image image;

    @BeforeEach
    void setup() throws IOException {
        BufferedImage bufferedImage = ImageIO.read(new File("src/main/resources/beach.png"));
        image = new Image(bufferedImage);
    }

    @Test
    void getGreenest() {
        Assertions.assertThat(image.getGreenest()).isEqualTo(7);
    }

    @Test
    void toBufferedImage() {
        AwtApprovals.verify(image.toBufferedImage());
    }

    @Test
    void highlightColumn() {
    // ===== END OF STARTING CODE =====
        List<Color> column = image.highlightColumn(3);
        // ===== STARTING CODE (provided by the module) =====
        Approvals.verify(column.toString());
        AwtApprovals.verify(image.toBufferedImage());
    }

    @Test
    void removeColumn() {
        List<Color> column = image.removeColumn(3);
        Approvals.verify(column.toString());
        AwtApprovals.verify(image.toBufferedImage());
    }

    @Test
    void insertColumn() {
        List<Color> column = List.of(
                Color.RED, Color.ORANGE, Color.YELLOW, Color.GREEN,
                Color.CYAN, Color.BLUE, Color.MAGENTA, Color.PINK);
        image.addColumn(2, column);
        AwtApprovals.verify(image.toBufferedImage());
    }
}
// ===== END OF STARTING CODE =====
