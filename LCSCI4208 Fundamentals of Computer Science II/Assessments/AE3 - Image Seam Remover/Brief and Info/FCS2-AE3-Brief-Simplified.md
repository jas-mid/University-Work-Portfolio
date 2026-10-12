# Fundamentals of Computer Science II AE3: Assessment Brief (Simplified)

Group Work

> [!NOTE]
> **About the code in this assessment:** this was a group project, built on starting code provided by the module. In the files in [`AE3 code`](../AE3%20code), every part that comes from that starting code is marked: blocks are wrapped in `// ===== STARTING CODE (provided by the module) =====` and `// ===== END OF STARTING CODE =====` comments, and single lines end with `// <- starting code`. Everything without a marker was written by the group: [Jasper Middleton (@jas-mid)](https://github.com/jas-mid), [Tom Woodford (@tw41158)](https://github.com/tw41158) and [@DheerB2303](https://github.com/DheerB2303). The group's original repository is [tw41158/AE3-Java](https://github.com/tw41158/AE3-Java).

## Summary

In this project, you will:

1. Write a program to compress images by removing seams rather than columns. Define seams as pixel sequences starting from the top row and selecting each subsequent pixel from the three available pixels below (down-left, down, down-right) based on either the highest green value or the lowest energy, continuing to the bottom row.
2. Allow users to remove either the greenest seam or the lowest-energy seam, highlighting the selected seam in green or red, respectively.
3. Develop a user interface enabling users to choose between greenest or lowest-energy seam deletion, confirm deletions, and perform undo operations.
4. Export the resulting images after each operation.

## Assessment Task

Continuing from AE2, groups of two to three students extend the image compression program to remove vertical seams instead of columns. Starter code is provided, and the task is to complete its missing parts so that the provided tests pass.

A vertical seam is a sequence of pixels from the top to the bottom of the image, where each pixel is directly or diagonally below the one before. The program removes either the greenest seam (the highest total green value) or the lowest-energy seam, which changes how recognisable the image is the least.

## Energy

A pixel's brightness is the average of its red, green and blue values. For a pixel E with neighbours:

```text
A B C
D E F
G H I
```

the horizontal and vertical energies are:

```text
horizontal(E) = (br(A) + 2br(D) + br(G)) − (br(C) + 2br(F) + br(I))
vertical(E)   = (br(A) + 2br(B) + br(C)) − (br(G) + 2br(H) + br(I))
```

and the energy at E combines the two. Pixels on the edges of the image don't have a full set of neighbours, so their brightness is used as their energy instead.

## Seam Finding

Checking every possible seam would be far too slow. Instead, use dynamic programming:

1. The first row's values are left as they are.
2. For each pixel in the next row, add the smallest of the (up to three) values directly or diagonally above it, and record which one it came from.
3. Repeat row by row to the bottom.
4. The smallest value in the bottom row is the end of the best seam. Trace back through the recorded links to find the whole seam.

The program must also undo removals by reinserting seams in reverse order, so removed seams and their exact positions need to be tracked.

## User Interface

The program responds to these keys, and users should be able to remove and restore several seams in a row:

- `g`: display the greenest seam (highlighted in green) and remove it.
- `e`: display the lowest-energy seam (highlighted in red) and remove it.
- `u`: undo the last deletion by reinserting the most recently removed seam.
- `q`: quit the program.

## Time Restrictions

Removing and reinserting a seam must each run in O(n) time, where n is the image height in pixels, so a standard 2D array or `ArrayList` is not enough; consider a graph structure. Updating pixel energies after a removal or insertion takes O(n²) time.

## Displaying Images

The program exports an image at each step:

- Greenest seam selected: the seam highlighted in green.
- Lowest-energy seam selected: the seam highlighted in red.
- Seam deleted: the image with the seam removed.
- Undo: the image after the most recently removed seam is reinserted.
