import { Box, Stack, Paper, Text, Flex, Badge, Menu, ActionIcon, UnstyledButton, Group } from '@mantine/core';
import { useDisclosure } from '@mantine/hooks';
import { useForm } from '@mantine/form';
import { IconStar, IconDots, IconPencil, IconTrash, IconPlus, IconCompass, IconActivity } from '@tabler/icons-react';
import PageTitle from '@/components/PageTitle';

const stars = [
  {
    name: "Do things",
    description: "yes",
    priority: "High",
    bearings: [
      {
        name: "Do thing A",
        description: "DO IT",
        movements: [
          {
            "name": "uhhh lemme think",
            "description": "i forgor"
          },
          {
            "name": "uhhh lemme think",
            "description": "i forgor"
          },
          {
            "name": "uhhh lemme think",
            "description": "i forgor"
          }
        ]
      },
      {
        name: "Do thing A",
        description: "DO IT",
        movements: [
          {
            "name": "uhhh lemme think",
            "description": "i forgor"
          },
          {
            "name": "uhhh lemme think",
            "description": "i forgor"
          },
          {
            "name": "uhhh lemme think",
            "description": "i forgor"
          }
        ]
      },
      {
        name: "Do thing A",
        description: "DO IT",
        movements: [
          {
            "name": "uhhh lemme think",
            "description": "i forgor"
          },
          {
            "name": "uhhh lemme think",
            "description": "i forgor"
          },
          {
            "name": "uhhh lemme think",
            "description": "i forgor"
          }
        ]
      }
    ]
  }
];
const priorityColors = {
  "High": "red",
  "None": "gray"
}
const goalColors = {
  "star": "red",
  "bearing": "green",
  "movement": "blue"
}

const Goal = ({ name, type, description, left, right }) => (
  <Paper p="sm" withBorder style={{ borderLeftWidth: "2px", borderLeftStyle: "solid", borderLeftColor: goalColors[type] }}>
    <Flex align="center" gap="sm">
      {left}
      <Box key="helop" flex={1}>
        <Text>{name}</Text>
        <Text size="xs" c="dimmed">{description}</Text>
      </Box>
      {right}
      <GoalMenu />
    </Flex>
  </Paper>
)

const GoalMenu = () => (
  <Menu>
    <Menu.Target>
      <ActionIcon variant="subtle" size="sm" aria-label="Open goal actions">
        <IconDots size={16} />
      </ActionIcon>
    </Menu.Target>
    <Menu.Dropdown>
      <Menu.Item leftSection={<IconPencil size={14} />}>Edit</Menu.Item>
      <Menu.Item leftSection={<IconTrash size={14} />} color="red">Delete</Menu.Item>
    </Menu.Dropdown>
  </Menu>
)

const onGoalAdd = () => {
  
}
const GoalAddButton = ({ text }) => (
  <UnstyledButton w="100%" onClick={onGoalAdd}>
    <Group gap="md">
      <IconPlus size={12} />
      <Text size="xs" c="dimmed">{text}</Text>
    </Group>
  </UnstyledButton>
)

export default function Goals() {
  const [opened, { open, close }] = useDisclosure(false);
  const form = useForm({
    mode: 'uncontrolled',
    initialValues: {
      email: '',
      termsOfService: false,
    },

    validate: {
      email: (value) => (/^\S+@\S+$/.test(value) ? null : 'Invalid email'),
    },
  });

  // TODO: Make tree structure more clear
  return (
    <Stack gap="sm">
      <PageTitle name="Stars" description="Goals, represented as spots in the galaxy." />

      {stars.map((star) => (
        <Stack>
          <Stack>
            <Goal
              name={star.name}
              type="star"
              description={star.description}
              left={<IconStar size={16} />}
              right={<Badge variant="light"
                color={priorityColors[star.priority]}>{star.priority}</Badge>}
            />

            <Stack pl="lg" style={{ borderLeftWidth: "2px", borderLeftStyle: "solid", borderLeftColor: goalColors["star"] }}>
              {star.bearings.map((bearing) =>
              (
                <Stack gap="sm">
                  <Goal
                    name={bearing.name}
                    type="bearing"
                    description={bearing.description}
                    left={<IconCompass size={14} />}
                  />

                  <Stack gap="xs" pl="lg" style={{ borderLeftWidth: "2px", borderLeftStyle: "solid", borderLeftColor: goalColors["bearing"] }}>
                    {bearing.movements.map((movement) =>
                    (
                      <Goal
                        name={movement.name}
                        type="movement"
                        description={movement.description}
                        left={<IconActivity size={14} />}
                      />
                    ))}

                    <GoalAddButton text="Add Movement" />
                  </Stack>
                </Stack>
              ))}

              <GoalAddButton text="Add Bearing" />
            </Stack>
          </Stack>
        </Stack>
      ))}
    </Stack>
  );
}