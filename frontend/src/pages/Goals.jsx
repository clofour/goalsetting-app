import { Box, Stack, Modal, Paper, Text, Flex, Badge, Menu, ActionIcon, UnstyledButton, Group } from '@mantine/core';
import { useDisclosure } from '@mantine/hooks';
import { useForm } from '@mantine/form';
import { IconStar, IconDots, IconPencil, IconTrash, IconPlus, IconCompass, IconActivity } from '@tabler/icons-react';
import PageTitle from '@/components/PageTitle';
import { useState } from 'react';

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



const CreateNorthStarForm = () => {

}

export default function Goals() {
  const [opened, { open, close }] = useDisclosure(false);
  const [activeForm, setActiveForm] = useState();
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

  const onGoalAdd = () => {
    setActiveForm("hello");
    open();
  }

  const GoalAddButton = ({ text }) => (
    <UnstyledButton w="100%" onClick={onGoalAdd}>
      <Group gap="md">
        <IconPlus size={12} />
        <Text size="xs" c="dimmed">{text}</Text>
      </Group>
    </UnstyledButton>
  )


  return (
    <Stack gap="sm">
      <PageTitle name="Stars" description="Goals, represented as spots in the galaxy." />

      <Modal opened={opened} onClose={close} title="Create GoalCard">
        {activeForm}
      </Modal>

      {stars.map((star) => (
        <Stack>
          <Stack>
            <GoalCard
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
                  <GoalCard
                    name={bearing.name}
                    type="bearing"
                    description={bearing.description}
                    left={<IconCompass size={14} />}
                  />

                  <Stack gap="xs" pl="lg" style={{ borderLeftWidth: "2px", borderLeftStyle: "solid", borderLeftColor: goalColors["bearing"] }}>
                    {bearing.movements.map((movement) =>
                    (
                      <GoalCard
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