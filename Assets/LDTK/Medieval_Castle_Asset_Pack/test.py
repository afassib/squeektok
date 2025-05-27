from PIL import Image
import os

# Folder containing your tiles
input_folder = "Tiles"
output_file = "merged_tileset.png"

# Get all tile images
tile_files = sorted([f for f in os.listdir(input_folder) if f.endswith(".png")])
tiles = [Image.open(os.path.join(input_folder, f)) for f in tile_files]

# Get tile size (assuming all tiles are the same size)
tile_width, tile_height = tiles[0].size

# Determine grid size (adjust columns as needed)
columns = 8  # Change based on how many tiles per row you want
rows = (len(tiles) + columns - 1) // columns  # Compute required rows

# Create a blank image for the final tileset
tileset = Image.new("RGBA", (columns * tile_width, rows * tile_height))

# Paste each tile into the tileset
for index, tile in enumerate(tiles):
    x = (index % columns) * tile_width
    y = (index // columns) * tile_height
    tileset.paste(tile, (x, y))

# Save the merged image
tileset.save(output_file)
print(f"Tileset saved as {output_file}")